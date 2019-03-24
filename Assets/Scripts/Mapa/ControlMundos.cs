using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMundos : MonoBehaviour {
    public GameObject mundo;
    public Transform jugador;
    public Mundos tipoMundo;
    
    // Use this for initialization
    void Start () {
        InformaGameManager();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InformaGameManager()
    {
        if (jugador != null && mundo != null) GameManager.instance.SetMundoYJugador(tipoMundo, mundo, jugador);
        Destroy(this);
    }
}
