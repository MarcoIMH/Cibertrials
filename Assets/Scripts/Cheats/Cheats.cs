using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public GameObject jugador;
    public Player tipoJugador;

    float gravedadPorDefecto = 0;

    bool volar = false, invencibilidad = false, gravedadRecogida = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Invencibilidad()
    {
        invencibilidad = !invencibilidad;
        if(invencibilidad) jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(-1);
        else jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(0.01f);
    }


    public void Volar()
    {
        if (!gravedadRecogida)
        {
            gravedadPorDefecto = jugador.GetComponent<Rigidbody2D>().gravityScale;
            gravedadRecogida = true;
        }            
        volar = !volar;
        jugador.GetComponent<ControladorJugador>().SetVolar(volar);
        if (volar) jugador.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        else jugador.GetComponent<Rigidbody2D>().gravityScale = gravedadPorDefecto;
    }
}
