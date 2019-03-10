using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour {

    public int segundosRespawn;

    PerdidasControl pc;
    /// <summary>
    /// Si colisiona con zonas de muerte, llama para cambiar la posicion de uno de los 2 jugadores
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Respawn respawn = other.GetComponent<Respawn>();
        if (respawn != null)
        {
            // Resetea stats del RigidBody y llama al método en el script respawn para cambiarle la posicion al jugador
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //CREO QUE ES MEJOR LLAMAR A UN METODO DEL JUGADOR (RESETEASTATS)
            other.GetComponent<Rigidbody2D>().angularVelocity = 0f;

            if (other.GetComponent<PerdidasControl>() != null)
            {
                pc = other.GetComponent<PerdidasControl>();
                pc.DesactivaControles(segundosRespawn); //si muere el jugador no puede volver a moverse hasta pasados "segundosRespawn" segundos
            }

            if (pc.EstablecerCongelado() && other.GetComponentInChildren<CubitoHielo>() != null)
            {
                Destroy(other.GetComponentInChildren<CubitoHielo>().gameObject); //si se encontraba congelado al morir se le quitael cubo de hielo
            }
            
            respawn.CambiarPos(other.transform);            
        }
    }
}
