﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerHijo : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.rotation = this.gameObject.transform.rotation;       //Rotamos el objeto para que quede bien apyoyado en la superficie
        other.gameObject.transform.SetParent(this.gameObject.transform.parent);         //Hacemos hijo al jugador del eje de giro
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.transform.rotation = Quaternion.Euler(Vector3.zero);      //Dejamos al jugador recto
        other.gameObject.transform.parent = null;       //Eliminamos la referencia de padre del jugador
    }
}