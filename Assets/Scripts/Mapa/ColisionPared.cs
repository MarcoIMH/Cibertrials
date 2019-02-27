using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionPared : MonoBehaviour {

    ControladorJugador controladorJugador; 

	// Use this for initialization
	void Start () {
        controladorJugador = GetComponentInParent<ControladorJugador>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Invertimos la velocidad en x del salto
        //controladorJugador.CambioVelocidadSaltoPared();
        // El jugador puede saltar en la pared
        //controladorJugador.CambiaSaltoPared(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // El jugador ya no puede saltar en la pared
        //controladorJugador.CambiaSaltoPared(false);
    }
}
