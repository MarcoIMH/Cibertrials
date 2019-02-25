using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidasControl : MonoBehaviour {

    bool controlesActivados = true;
    bool reduceVelocidad = false;

    ControladorJugador controles;

    private void Start()
    {
        if(this.gameObject.GetComponent<ControladorJugador>()!=null)
        controles = this.gameObject.GetComponent<ControladorJugador>();
    }

    public void ActivaControles()
    {
        controles.SetActivaControles(true);
    }

    public void DesactivaControles(int segundos)
    {
        controles.SetActivaControles(false);
        Invoke("ActivaControles", segundos);
    }

    public void ActivaReduceVelocidad()
    {
        controles.SetActivaReduceVelocidad(true);
    }

    public void DesactivaReduceVelocidad(int segundos)
    {
        controles.SetActivaReduceVelocidad(false);
        Invoke("ActivaControles", segundos);
    }
}
