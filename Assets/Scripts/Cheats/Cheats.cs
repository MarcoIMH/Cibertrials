using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public GameObject jugador;
    public Player tipoJugador;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Invencibilidad()
    {
        jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(-1);
    }

}
