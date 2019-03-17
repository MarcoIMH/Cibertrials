using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujeInicial : MonoBehaviour {

    Rigidbody2D rb;
    public float empuje;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        //empuja el tronco en la direccion donde mira el objeto generador 
        //un poco al principio para separarlo del generador y que caiga por la rampa 
        rb.AddForce(new Vector2(empuje * transform.lossyScale.x, 0), ForceMode2D.Impulse);
	}
}
