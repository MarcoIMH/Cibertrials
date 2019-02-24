using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    Transform spawn;

    void Start()
    {

    }

    /// <summary>
    /// Recoloca al jugador en el ultimo punto guardado
    /// </summary>
    /// <param name="jugador"></param>
    public void CambiarPos()
    {
        transform.position = spawn.position;
    }

    /// <summary>
    /// Actualiza el spawnpoint de los jugadores
    /// </summary>
    /// <param name="posicionNueva"></param> nueva posicion
    /// <param name="jugador"></param>
    public void CambiaSpawn(Transform posicionNueva)
    {
        spawn = posicionNueva;
    }
}
