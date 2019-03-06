using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidasControl : MonoBehaviour {

    //segundos que dura la modificacion de velocidad
    public float  segundosModificaVelocidad, segundosInversionControles;

    //acceso al prefab del cubo de hielo para instanciarlo
    public CubitoHielo cuboHielo;

    //variable que indica si el jugador esta congelado
    //se usa para activar o desactivar los controles del jugador
    bool congelado = false;

    ControladorJugador controles;

    private void Start()
    {
        //acceso al controlador jugador para llamar al switch
        if(this.gameObject.GetComponent<ControladorJugador>()!=null)
        controles = this.gameObject.GetComponent<ControladorJugador>();

        
    }

    /// <summary>
    /// cambia el estado de los controles a true
    /// </summary>
    public void ActivaControles()
    {
        controles.CambiosPerdidaControl(PerdidaControles.stun, 1, true);
    }

    /// <summary>
    /// cambia el estado de los controles a false
    /// </summary>
    /// <param name="segundos"></param>
    public void DesactivaControles(int segundos)
    {
        //desactiva los controles
        controles.CambiosPerdidaControl(PerdidaControles.stun, 1, false);
        //los vuelve a activar pasados "segundos" segundos
        Invoke("ActivaControles", segundos);
    }

    /// <summary>
    /// modifica la velocidad
    /// </summary>
    /// <param name="velocidadModificada"></param>
    public void ActivaModificaVelocidad( float velocidadModificada)
    {
        //modifica la velocidad multiplicandola por "velocidadModificada"
        controles.CambiosPerdidaControl(PerdidaControles.ralentizar, velocidadModificada, true);
        //la devuelve a su valor normal pasados "segundosModificaVelocidad" segundos
        Invoke("DesactivaModificaVelocidad", segundosModificaVelocidad);
    }

    /// <summary>
    /// restaura la velocidad del jugador
    /// </summary>
    public void DesactivaModificaVelocidad()
    {
        controles.RestauraVelocidad(); 
    }

    /// <summary>
    /// //activa el cubode hielo y congela al jugador poniendo el estado de los controles a false
    /// </summary>
    public void AplicarCuboDeHielo()
    {
        congelado = true;
        //pone el estado de los controles a false
        controles.CambiosPoderes(Poderes.cubito, congelado);
        //instancia el cubo de hielo entrando su script en ejecucion 
        CubitoHielo newCuboHielo = Instantiate<CubitoHielo>(cuboHielo, transform);
    }

    /// <summary>
    /// reactiva los controles
    /// </summary>
    public void DesactivarCuboDeHielo()
    {
        congelado = false;
        controles.CambiosPoderes(Poderes.cubito, congelado);
    }

    /// <summary>
    /// Invierte la velocidad en X e intercambia las teclas de rodar y saltar
    /// </summary>
    public void ActivaInvierteControles()
    {
        //hace dichos cambios
        controles.CambiosPoderes(Poderes.inversionControles, false);
        //los revierte pasados "segundos" segundos
        Invoke("DesactivaInvierteControles", segundosInversionControles);
    }

    /// <summary>
    /// Invierte la velocidad en X e intercambia las teclas de rodar y saltar
    /// Hace lo mismo que el metodo de activar pero si no el invoke entraria en bucle
    /// </summary>
    public void DesactivaInvierteControles()
    {
        controles.CambiosPoderes(Poderes.inversionControles, false);
    }
    public bool EstablecerCongelado()
    {
        return congelado;
    }

}
