﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControles : MonoBehaviour {

    public ControlesManager controlesManagerJ1, controlesManagerJ2;
    public Text cabecera, saltar, rodar, poder, romperParedes, izqdaParedes, dchaParedes, menu;
    public Player jugadorQueAbreMenu;

    GameObject boton;
    Dictionary<string, KeyCode> controles = new Dictionary<string, KeyCode>();

    KeyCode nuevaTecla;
    string nombreTecla;    
    bool cambiaTecla=false, enMenuPrincipal;

    // Use this for initialization
    void Start () {
        enMenuPrincipal = Controles.instance.GetEnMenuPrincipal();
	}
	
	// Update is called once per frame
	void Update () {
        if (cambiaTecla && !enMenuPrincipal)
        {
            if (jugadorQueAbreMenu == Player.jugador1 && controlesManagerJ1 != null) controlesManagerJ1.CambiaControlJugador(nombreTecla, nuevaTecla);
            else if(controlesManagerJ2 != null) controlesManagerJ2.CambiaControlJugador(nombreTecla, nuevaTecla);

            cambiaTecla = false;
        }else if(cambiaTecla && enMenuPrincipal)
        {
            Controles.instance.SetControlesJugador(controles, jugadorQueAbreMenu);    
            cambiaTecla = false;
        }
	}

    private void OnGUI()
    {
        if (boton != null)
        {
            Event teclaPulsada = Event.current;
            if (teclaPulsada.isKey)
            {                
                controles[boton.name] = teclaPulsada.keyCode;
                boton.GetComponentInChildren<Text>().text = teclaPulsada.keyCode.ToString();
                nombreTecla = boton.name;
                nuevaTecla = teclaPulsada.keyCode;

                boton.GetComponent<Image>().color = boton.GetComponent<Button>().colors.normalColor;
                boton = null;
                cambiaTecla = true;
            }
        }
    }

    void SetDiccionarioJugador(Dictionary<string, KeyCode> controles)
    {
        this.controles = controles;
    }

    void ConfiguraTextos()
    {
        saltar.text = controles["Saltar"].ToString();
        rodar.text = controles["Rodar"].ToString();
        poder.text = controles["Poder"].ToString();
        romperParedes.text = controles["RomperParedes"].ToString();
        izqdaParedes.text = controles["IzqdaParedes"].ToString();
        dchaParedes.text = controles["DchaParedes"].ToString();
        menu.text = controles["Menu"].ToString();
    }
    
    public void CambiaTecla(GameObject botonPulsado)
    {
        if (botonPulsado!= null)
        {
            botonPulsado.GetComponent<Image>().color = Color.cyan;
            boton = botonPulsado;
        }
    }

    public void CargaMenuControles(Player tipoJugador)
    {
        enMenuPrincipal = Controles.instance.GetEnMenuPrincipal();

        controles = Controles.instance.GetControlesJugador(tipoJugador);
        SetDiccionarioJugador(controles);
        ConfiguraTextos();

        if (tipoJugador == Player.jugador1) cabecera.text = "CONTROLES JUGADOR 1";
        else cabecera.text = "CONTROLES JUGADOR 2";

        jugadorQueAbreMenu = tipoJugador;
    }

    public void CargaMenuOutGame()
    {
        enMenuPrincipal = Controles.instance.GetEnMenuPrincipal();

        controles = Controles.instance.GetControlesJugador(jugadorQueAbreMenu);
        SetDiccionarioJugador(controles);
        ConfiguraTextos();

        if (jugadorQueAbreMenu == Player.jugador1) cabecera.text = "CONTROLES JUGADOR 1";
        else cabecera.text = "CONTROLES JUGADOR 2";
    }
}
