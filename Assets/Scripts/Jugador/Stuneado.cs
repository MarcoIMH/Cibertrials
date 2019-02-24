using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuneado : MonoBehaviour {

    // Este script debería estar dentro del ControladorJugador

    // Velocidad del PlayerController
    public float velocidadX;

    // Si está o no estuneado
    bool estuneado;

    Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if (!estuneado)
        {
            // El jugador se mueve
        }
    }

    /// <summary>
    /// Este metodo se llama si el enemigo goomba colisiona por trigger con el jugador. 
    /// Reproduce un Stun al jugador que a los 2 segundos se va
    /// </summary>
    public void ReproducirStun()
    {
        // rb.velocity.x / 5 para que el jugador no se pare en seco sino que haga una rápida transición a pararse
        estuneado = true;
        // a los 2 segundos se llama al método StopStun, que le devuelve al jugador el control
        Invoke("PararStun", 2);   
    }

    /// <summary>
    /// Le devuelve al jugador el control de movimiento 
    /// </summary>
    void PararStun()
    {
        estuneado = false;
    }

   
}
