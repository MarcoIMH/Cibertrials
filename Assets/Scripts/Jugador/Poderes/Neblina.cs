using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neblina : MonoBehaviour {
    public float cantidadRalentizar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        ControladorJugador controlador;
        PerdidasControl pc = other.GetComponent<PerdidasControl>();
        if (pc != null)
        {
            pc.ActivaModificaVelocidad(cantidadRalentizar);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ControladorJugador controlador;
        PerdidasControl pc = other.GetComponent<PerdidasControl>();
        if (pc != null)
        {
            pc.DesactivaModificaVelocidad();
        }
    }
}
