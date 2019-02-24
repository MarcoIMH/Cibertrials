using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaStun : MonoBehaviour {

    // para acceder al script del jugador 
    ControladorJugador controladorJugador;

    /// <summary>
    /// Este metodo se llama si el enemigo goomba colisiona por trigger con algun otro objeto
    /// </summary>
    /// /// <param name="other"></param> El que ha colisionado con el enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que ha chocado es alguno de los jugadores
        if (other.gameObject.CompareTag("jugador1") || other.gameObject.CompareTag("jugador2"))
        {
            // Accedes al script playerController del jugador
            controladorJugador = other.gameObject.GetComponent<PlayerController>();

            // Solo usarlo si existe para evitar errores en ejecución
            if (controladorJugador != null)
            {
                // Activa el método para reproducir el Stun en el jugador

                // Ahora el método PlayStun está hecho en el script "Stuneado" ----- Cambiar, deberia estar hecho en el PlayerController
                controladorJugador.ReproducirStun();
                Debug.Log("Stun provocado a alguno de los jugadores");
            }
        }
    }
}
