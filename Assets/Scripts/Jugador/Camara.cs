using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

    public Transform transformJugador;
    public float distanciaX, distanciaY;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        if (transformJugador != null)
        {
            transform.position = new Vector3(transformJugador.position.x + distanciaX, transformJugador.position.y + distanciaY, transformJugador.position.z);
        }
    }
}