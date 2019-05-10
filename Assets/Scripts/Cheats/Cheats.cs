using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public GameObject jugador;
    public Player tipoJugador;


    Transform[] coordsRespawns;
    float gravedadPorDefecto = 0;
    bool volar = false, invencibilidad = false, gravedadRecogida = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Invencibilidad()
    {
        invencibilidad = !invencibilidad;
        CheatsManager.instance.SetEstadoInvencibilidad(invencibilidad);
        if(invencibilidad) jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(-1);
        else jugador.GetComponent<EstadoFantasma>().ActivaEstadoFantasma(0.01f);
    }

    public bool Getinvencibilidad()
    {
        return invencibilidad;
    }

    public void Volar()
    {
        if (!gravedadRecogida)
        {
            gravedadPorDefecto = jugador.GetComponent<Rigidbody2D>().gravityScale;
            gravedadRecogida = true;
        }            
        volar = !volar;
        jugador.GetComponent<ControladorJugador>().SetVolar(volar);
        if (volar) jugador.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        else jugador.GetComponent<Rigidbody2D>().gravityScale = gravedadPorDefecto;
    }

    public void SiguienteRespawn()
    {
        coordsRespawns = GameManager.instance.GetCoordenadasPoderes();
        int indice = 1;
        while (indice + 2 < coordsRespawns.Length && jugador.transform.position.x >= coordsRespawns[indice].position.x) indice++;
        if(indice+2 < coordsRespawns.Length)
        {
            Vector3 posicion;
            if (tipoJugador == Player.jugador1)
                posicion = new Vector3(coordsRespawns[indice].localPosition.x, coordsRespawns[indice].localPosition.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
            else posicion = new Vector3(coordsRespawns[indice].position.x, coordsRespawns[indice].position.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
            jugador.transform.position = posicion;
        }            
    }

    public void UltimoRespawn()
    {
        coordsRespawns = GameManager.instance.GetCoordenadasPoderes();
        Vector3 posicion;
        int indice = coordsRespawns.Length-2;
        if (tipoJugador == Player.jugador1)
            posicion = new Vector3(coordsRespawns[indice].localPosition.x, coordsRespawns[indice].localPosition.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
        else posicion = new Vector3(coordsRespawns[indice].position.x, coordsRespawns[indice].position.y + coordsRespawns[indice].transform.localScale.y * 2, 0);
        jugador.transform.position = posicion;
    }
}
