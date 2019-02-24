using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    Transform spawn;

    void Start()
    {

    }

    private void Update()
    {
        Debug.Log(spawn.position.x);
    }

    /// <summary>
    /// Recoloca al jugador en el ultimo punto guardado
    /// </summary>
    /// <param name="jugador"></param>
    public void CambiarPos(Transform other)
    {
        other.position = spawn.position;
        //transform.position = spawn.position;
    }

    /// <summary>
    /// Actualiza el spawnpoint de los jugadores
    /// </summary>
    /// <param name="posicionNueva"></param> nueva posicion
    /// <param name="jugador"></param>
    public void CambiaSpawn(Transform posicionNueva)
    {
        //print("holo");
        spawn = posicionNueva;
    }
}
