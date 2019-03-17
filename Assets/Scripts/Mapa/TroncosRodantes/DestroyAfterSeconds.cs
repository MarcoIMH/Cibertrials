using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public float tiempoDeVida;

	// Use this for initialization
	void Start () {
        //Destruye el tronco a los tiempoDeVida segundos
        Destroy(this.gameObject, tiempoDeVida);
	}
	
}
