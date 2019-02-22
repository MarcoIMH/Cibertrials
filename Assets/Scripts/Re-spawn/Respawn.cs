using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public CambioSpawn cambioSpawn;             //Referencia a uno de los triggers para tener el spawnpoint
	
    public void CambiarPos()
    {
        transform.position = cambioSpawn.puntoSpawn.position;                   //Posiciona al jugador en la posicion del objeto vacio SpawnPoint
    }
}
