using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //Declaracion de ambos jugadores
    Player player1, player2;

    //variables que indican el numero de rondas ganadas por cada jugador
    int roundsPlayer1;
    int roundsPlayer2;

    //Asegurarse de que solo hay una instancia
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        //Ambos jugadores comienzan la partida con 0 rondas ganadas
        roundsPlayer1 = roundsPlayer2 = 0;

        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// este método se llama cuando uno de los jugadores alcanza la llegada (arrival)
    /// </summary>
    /// <param name="winner"></param>
    public void EndRound(Player winner)
    {
        if(winner == player1)
        {
            roundsPlayer1++;
        }
        else roundsPlayer2++;

        //-----------> aqui habría que desactivar el componente "playercontroller" de ambos jugadores
    }
}
