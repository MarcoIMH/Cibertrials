using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidasControl : MonoBehaviour {

    //segundos que dura la modificacion de velocidad
    public float segundosModificaVelocidad;

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
        controles.SetEstadoControlador(true);
    }

    /// <summary>
    /// cambia el estado de los controles a false
    /// </summary>
    /// <param name="segundos"></param>
    public void DesactivaControles(float segundos)
    {
        //desactiva los controles
        controles.ReseteaStats();
        controles.SetEstadoControlador(false);        
        //los vuelve a activar pasados "segundos" segundos. Utilizamos el valor (-1) para indicar que no los vuelva a activar porque se hará sin invoke cuando corresponda.
        if(segundos!=-1) Invoke("ActivaControles", segundos);
    }

    /// <summary>
    /// modifica la velocidad
    /// </summary>
    /// <param name="velocidadModificada"></param>
    public void ActivaModificaVelocidad(float velocidadModificada)
    {
        //modifica la velocidad multiplicandola por "velocidadModificada"
        controles.ModificaVelocidad(velocidadModificada);
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
}
