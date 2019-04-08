using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaDeCarga : MonoBehaviour {

    public Sprite[] piePantallaCarga;

    public GameObject imagenCargando, jugador1, jugador2;

    public Text textoConsejo;

    int mapa;

	// Use this for initialization
	void Start () {
        textoConsejo.text = ConsejoDinamico();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string ConsejoDinamico()
    {
        int indice = Random.Range(0, 4);
        string[] consejos = new string[3];
        consejos[0] = "Consejo: No podrás salir hasta que consigas la llave...";
        consejos[1] = "Consejo: Usa tus poderes sabiamente, pero úsalos!";
        consejos[2] = "Consejo: Cuidado, hay pelotas traicioneras y mocos con mala leche..";
        consejos[3] = "Consejo: Corre, corre,corre! Gana el que llega antes a la meta!!";
        return consejos[indice];
    }

    void CambiaPieDeCarga()
    {     
        textoConsejo.enabled = true;
        imagenCargando.SetActive(true);
        imagenCargando.GetComponent<Image>().sprite = piePantallaCarga[mapa - 1];

        jugador1.SetActive(false);
        jugador2.SetActive(false);
    }

    public void MostrarResultados(int rondasJ1, int rondasJ2, int mapa, float tiempoMostrarResultados)
    { 
        textoConsejo.enabled = false;
        imagenCargando.SetActive(false);

        jugador1.SetActive(true);
        jugador2.SetActive(true);

        jugador1.GetComponentInChildren<Text>().text = "" + rondasJ1;
        jugador2.GetComponentInChildren<Text>().text = "" + rondasJ2;

        this.mapa = mapa;

        Invoke("CambiaPieDeCarga", tiempoMostrarResultados/2);
    }
}
