using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public ChangeSpawn changeSpawn;             //Referencia a uno de los triggers para tener el spawnpoint
	
    public void ChangePos()
    {
        transform.position = changeSpawn.spawnpoint.position;                   //Posiciona al jugador en la posicion del objeto vacio SpawnPoint
    }
}
