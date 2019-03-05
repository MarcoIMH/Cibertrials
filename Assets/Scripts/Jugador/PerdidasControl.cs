using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidasControl : MonoBehaviour {

    //segundos que dura la modificacion de velocidad
    public float  segundosModificaVelocidad;

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

        //AplicarCuboDeHielo();
    }

    //cambia el estado de los controles a true
    public void ActivaControles()
    {
        controles.CambiosPerdidaControl(PerdidaControles.stun, 1, true);
    }

    //cambia el estado de los controles a false
    public void DesactivaControles(int segundos)
    {
        //desactiva los controles
        controles.CambiosPerdidaControl(PerdidaControles.stun, 1, false);
        //los vuelve a activar pasados "segundos" segundos
        Invoke("ActivaControles", segundos);
    }

    //modifica la velocidad
    public void ActivaModificaVelocidad( float velocidadModificada)
    {
        //modifica la velocidad
        controles.CambiosPerdidaControl(PerdidaControles.ralentizar, velocidadModificada, true);
        //la devuelve a su valor normal pasados "segundosModificaVelocidad" segundos
        Invoke("DesactivaModificaVelocidad", segundosModificaVelocidad);
    }

    //restaura la velocidad del jugador
    public void DesactivaModificaVelocidad()
    {
        controles.RestauraVelocidad(); 
    }

    //activa el cubode hielo y congela al jugador poniendo el estado de los controles a false
    public void AplicarCuboDeHielo()
    {
        congelado = true;
        //pone el estado de los controles a false
        controles.CambiosPoderes(Poderes.cubito, congelado);
        //instancia el cubo de hielo entrando su script en ejecucion 
        CubitoHielo newCuboHielo = Instantiate<CubitoHielo>(cuboHielo, transform);
    }

    //reactiva los controles
    public void DesactivarCuboDeHielo()
    {
        congelado = false;
        controles.CambiosPoderes(Poderes.cubito, congelado);
    }
}
