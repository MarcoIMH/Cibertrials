using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image[] gemaJugador = new Image[6]; //0...2 player1, de 3...5 player2

    //Descomentar cuando tengamos una imagen de la llave para la UI
    //public Image[] imagenLlaves; //0 -> jugador 1 , 1 -> jugador 2
    int gemasMax;

    void Start()
    {

        GameManager.instance.SetUI(this);
        gemasMax = gemaJugador.Length / 2;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActualizaGema(int gema, Player jugador)
    {

        if (gema == 0)
        {
            int min, max;
            if (jugador == Player.jugador1)
            {
                min = 0;
                max = gemasMax;
            }
            else
            {
                min = gemasMax;
                max = gemaJugador.Length;
            }
            for (int i = min; i < max; i++)
            {
                gemaJugador[i].gameObject.SetActive(false);
            }
        }
        else
        {
            if (jugador == Player.jugador1) gemaJugador[gema - 1].gameObject.SetActive(true);
            else gemaJugador[gema + gemasMax - 1].gameObject.SetActive(true);
        }
    }

    //Descomentar cundo tengammos una imagen de la llave para la UI
    /*
    /// <summary>
    /// Metodo que sirve para activar o desactivar la imagen de la llave de la UI
    /// </summary>
    /// <param name="jugador">para diferenciar a los jugadores</param>
    /// <param name="activado">para activar o desactivar la imagen de la llave</param>
    public void ActualizarLlave(Player jugador, bool activado)
    {
        if (jugador == Player.jugador1)
        {
            imagenLlaves[0].gameObject.SetActive(activado);
        }
        else
        {
            imagenLlaves[1].gameObject.SetActive(activado);
        }
    }
    */
}
