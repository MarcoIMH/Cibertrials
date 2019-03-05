using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraVelocidad : MonoBehaviour {

    public float duracion = 5f;
    public float aumentoVelocidad = 20f;

	void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}

    /// <summary>
    /// Cuando el jugador colisiona con el powerUp se le aumenta la velocidad durante un tiempo
    /// y luego restaura a su valor original y finalmente se destruye el powerUp
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ControladorJugador controlador = collision.gameObject.GetComponent<ControladorJugador>();

        if (controlador != null)
        {
            controlador.AumentaVelocidad(aumentoVelocidad, duracion);
            GameManager.Destroy(this.gameObject);
        }
    }
}
