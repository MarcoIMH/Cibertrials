using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour {


    /// <summary>
    /// Si colisiona con zonas de muerte, llama para cambiar la posicion de uno de los 2 jugadores
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Respawn respawn = other.GetComponent<Respawn>();
        if (respawn != null)
        {
             // Llama al método en el script respawn para cambiarle la posicion al jugador
             respawn.CambiarPos(other.transform);
             print("patata");
        }
    }
}
