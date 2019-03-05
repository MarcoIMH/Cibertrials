using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image[] gemaJugador = new Image[6]; //0...2 player1, de 3...5 player2
    int gemasMax;
    void Start () {

        GameManager.instance.SetUI(this);
        gemasMax = gemaJugador.Length / 2;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActualizaGema(int gema, Player jugador)
    {
 
        if (gema == 0)
        {
            int min, max;
            if(jugador == Player.jugador1)
            {
                min = 0;
                max = gemasMax;
            }
            else
            {
                min = gemasMax;
                max = gemaJugador.Length;
            }
            for(int i = min; i < max; i++)
            {
                gemaJugador[i].gameObject.SetActive(false);
            }
        }
        else
        {
            if (jugador == Player.jugador1) gemaJugador[gema-1].gameObject.SetActive(true);
            else gemaJugador[gema + gemasMax-1].gameObject.SetActive(true);
        }
    }

}
