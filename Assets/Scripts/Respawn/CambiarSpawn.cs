using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSpawn : MonoBehaviour {

    /// <summary>
    /// Al colisionar con el jugador, actualiza el spawnpoint de respawn
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //respawn = other.
        if(other.gameObject.respawn()!=null){
            other.gameObject.respawn.CambiarSpawn(other.transform);
        }

     }
}
