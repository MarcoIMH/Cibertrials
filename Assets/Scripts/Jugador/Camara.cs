using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    public Transform transformJugador;
    Vector3 distancia;

    private void Start()
    {
       
        distancia = transform.position - transformJugador.position;
    }

    private void LateUpdate()
    {
        transform.position = transformJugador.position + distancia;
    }
}
