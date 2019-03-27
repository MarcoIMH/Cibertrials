using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image[] gemaJugador; //0..v.lentgh/2-1 -->Jug1, v.Lentgh/2...v.Lentgh-1 -->Jug2
    public Image[] imagenLlaves; //0 -> jugador 1 , 1 -> jugador 2
    int gemasMax;

    void Start()
    {
        GameManager.instance.SetUI(this);
        gemasMax = gemaJugador.Length / 2;
    }
    public void ActualizaGema(int gema, Player jugador, Poderes poder)
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
    
}
