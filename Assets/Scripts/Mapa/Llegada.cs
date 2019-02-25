using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llegada : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Este metodo llama al final de la ronda en el GAme Manager cuando el jugador toca la meta
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("jugador1"))
        {
            GameManager.instance.FinalizarRonda(Player.jugador1);
            Debug.Log(" PLAYER 1 WINS");
        }
        else if (other.gameObject.CompareTag("jugador2"))
        {
            GameManager.instance.FinalizarRonda(Player.jugador2);
            Debug.Log(" PLAYER 2 WINS");
        }
    }

}
