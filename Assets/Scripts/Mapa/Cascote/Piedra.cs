using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : MonoBehaviour {

    public float vCaida;
    public float tDestruccion;

    bool cae;
    Vector2 mov;

	// Use this for initialization
	void Start ()
    {
        mov = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(mov * Time.deltaTime);
	}

    public void IniciaMov()
    {
        mov = new Vector2(0, -vCaida);
        Destroy(GetComponentInParent<Transform>().gameObject, tDestruccion);
    }
}
