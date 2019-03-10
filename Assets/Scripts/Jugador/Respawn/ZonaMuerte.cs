﻿using System.Collections;
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
            if(other.gameObject.GetComponent<ControladorJugador>() != null)
            {
                other.gameObject.GetComponent<ControladorJugador>().ReseteaStats();
            }

            if (other.GetComponent<PerdidasControl>() != null)
            {
                pc = other.GetComponent<PerdidasControl>();
                pc.DesactivaControles(segundosRespawn); //si muere el jugador no puede volver a moverse hasta pasados "segundosRespawn" segundos
            }
            
            //FALTA QUE CUANDO MUERES SE TE QUITAN LOS CCS

            respawn.CambiarPos(other.transform);            
        }
    }
}
