using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControles : MonoBehaviour {

    public ControlesManager controlesManagerJ1, controlesManagerJ2;

    public Text cabecera, saltar, rodar, poder, romperParedes, izqdaParedes, dchaParedes, menu;

    Dictionary<string, KeyCode> controles = new Dictionary<string, KeyCode>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetDiccionarioJugador(Dictionary<string, KeyCode> controles)
    {
        this.controles = controles;
    }

    void CargaMenuControles()
    {
        saltar.text = controles["Saltar"].ToString();
        rodar.text = controles["Rodar"].ToString();
        poder.text = controles["Poder"].ToString();
        romperParedes.text = controles["RomperParedes"].ToString();
        izqdaParedes.text = controles["IzqdaParedes"].ToString();
        dchaParedes.text = controles["DchaParedes"].ToString();
        menu.text = controles["Menu"].ToString();
    }

    public void AbreMenuconfiguracion(Dictionary<string, KeyCode> controles, Player tipoJugador)
    {
        SetDiccionarioJugador(controles);
        CargaMenuControles();

        if (tipoJugador == Player.jugador1) cabecera.text = "CONTROLES JUGADOR 1";
        else cabecera.text = "CONTROLES JUGADOR 2";
    }
}
