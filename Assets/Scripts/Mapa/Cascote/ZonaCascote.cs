using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaCascote : MonoBehaviour {
 
    Piedra piedra;

	// Use this for initialization
	void Start ()
    {
        if(GetComponentInChildren<Piedra>() != null)
        {
            piedra = GetComponentInChildren<Piedra>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)//cuando el jugador entra en la zona
    {
        piedra.IniciaMov();
    }

}
