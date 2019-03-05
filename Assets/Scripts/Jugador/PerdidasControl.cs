using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerdidasControl : MonoBehaviour {

    public float  segundosModificaVelocidad;
    public CubitoHielo cuboHielo;

    bool congelado = false;

    ControladorJugador controles;

    private void Start()
    {
        if(this.gameObject.GetComponent<ControladorJugador>()!=null)
        controles = this.gameObject.GetComponent<ControladorJugador>();

        AplicarCuboDeHielo();
    }

    public void ActivaControles()
    {
        controles.CambiosPerdidaControl(PerdidaControles.stun, 1, true);
    }

    public void DesactivaControles(int segundos)
    {
        controles.CambiosPerdidaControl(PerdidaControles.stun, 1, false);
        Invoke("ActivaControles", segundos);
    }

    public void ActivaModificaVelocidad( float velocidadModificada)
    {
        controles.CambiosPerdidaControl(PerdidaControles.ralentizar, velocidadModificada, true);
        Invoke("DesactivaModificaVelocidad", segundosModificaVelocidad);
    }

    public void DesactivaModificaVelocidad()
    {
        controles.RestauraVelocidad();
        
    }

    public void AplicarCuboDeHielo()
    {
        congelado = true;
        controles.CambiosPoderes(Poderes.cubito, congelado);
        CubitoHielo newCuboHielo = Instantiate<CubitoHielo>(cuboHielo, transform);
    }

    public void DesactivarCuboDeHielo()
    {
        congelado = false;
        controles.CambiosPoderes(Poderes.cubito, congelado);
    }
}
