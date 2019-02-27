using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour {

    public int segundosRespawn;
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
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Rigidbody2D>().angularVelocity = 0f;

            if (other.GetComponent<PerdidasControl>() != null)
            {
                other.GetComponent<PerdidasControl>().DesactivaControles(segundosRespawn);
            }
            respawn.CambiarPos(other.transform);             
        }
    }
}
