using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : MonoBehaviour {

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
        if (other.gameObject.CompareTag("player1"))
        {
            GameManager.instance.EndRound(Player.player1);
            Debug.Log(" PLAYER 1");
        }
        else if (other.gameObject.CompareTag("player2"))
        {
            GameManager.instance.EndRound(Player.player2);
            Debug.Log(" PLAYER 2");
        }
    }

}
