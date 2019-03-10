using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanderaRodar : MonoBehaviour {

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //hace que el jugador no pueda rodar
        ControladorJugador cj = other.GetComponent<ControladorJugador>();
        if (cj != null)
        {
            cj.CheckDejarRodar(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //hace que el jugador pueda rodar
        ControladorJugador cj = other.GetComponent<ControladorJugador>();
        if (cj != null)
        {
            cj.CheckDejarRodar(false);

        }
    }
}
