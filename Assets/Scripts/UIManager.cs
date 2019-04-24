using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuIngame, pantallaDeCarga, pantallaGanador;
    public MenuControles menuControles;

    public Image[] gemaJugador; //0..v.lentgh/2-1 -->Jug1, v.Lentgh/2...v.Lentgh-1 -->Jug2
    public Image[] poderesJug;
    public Image[] imagenLlaves; //0 -> jugador 1 , 1 -> jugador 2
    int gemasMax, poderesMax;
    float tiempo=0;
    bool activarTiempo = false;

    void Start()
    {
        GameManager.instance.SetUI(this);
        gemasMax = gemaJugador.Length / 2;
        poderesMax = poderesJug.Length / 2;
    }

    private void Update()
    {
        if (activarTiempo)
        {
            tiempo -= Time.deltaTime;
            if (tiempo < 0)
            {
                activarTiempo = false;
                GameManager.instance.ColocaJugadores();
                CierraPantallaDeCarga();
            }
        }
    }

    public void ActualizaGema(int gema, Player jugador, Poderes poder)
    {
        //Gemas
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
            if (jugador == Player.jugador1)
            {
                gemaJugador[gema - 1].gameObject.SetActive(true);
            }
            else gemaJugador[gema + gemasMax - 1].gameObject.SetActive(true);
        }
        //Poder
        if (poder == Poderes.sinPoder)
        {
            int min, max;
            if (jugador == Player.jugador1)
            {
                min = 0;
                max = poderesMax;
            }
            else
            {
                min = poderesMax;
                max = poderesJug.Length;
            }
            for (int i = min; i < max; i++)
            {
                poderesJug[i].gameObject.SetActive(false);
            }
        }
        else
        {
            if (jugador == Player.jugador1) poderesJug[(int)poder].gameObject.SetActive(true);
            else poderesJug[poderesJug.Length/2+(int)poder].gameObject.SetActive(true);
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

    public void AbreMenuIngame(Player jugador)
    {
        if(jugador == Player.jugador1) menuControles.CargaMenuControles(Player.jugador1);
        else menuControles.CargaMenuControles(Player.jugador2);

        this.gameObject.SetActive(false);
        menuIngame.SetActive(true);
    }

    public void CierraMenuIngame()
    {
        menuIngame.SetActive(false);
        this.gameObject.SetActive(true);
        GameManager.instance.QuitaPausaJuego();
    }

    public void AbrePantallaDeCarga(float tiempo)
    {
        pantallaDeCarga.SetActive(true);
        this.tiempo = tiempo;
        activarTiempo = true;
    }

    public void CierraPantallaDeCarga()
    {
        pantallaDeCarga.SetActive(false);
    }

    public void CargaResultados(int rondasJ1, int rondasJ2, int mapa, float tiempoMostrarResultados)
    {
        AbrePantallaDeCarga(tiempoMostrarResultados);
        pantallaDeCarga.GetComponent<PantallaDeCarga>().MostrarResultados(rondasJ1, rondasJ2, mapa, tiempoMostrarResultados);
    }

    public void AbrePantallaGanador(Player jugador)
    {
        pantallaGanador.GetComponent<PantallaGanador>().SetGanador(jugador);
        pantallaGanador.SetActive(true);
    }
}
