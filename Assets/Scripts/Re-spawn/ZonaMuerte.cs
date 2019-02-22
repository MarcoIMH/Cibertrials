using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour {
    
	void OnTriggerEnter2D(Collider2D other)
    {
       Respawn respawn = other.GetComponent<Respawn>();  //Comprobar si tiene el script Respawn y si se verifica ejecutar el ChangePos
            if (respawn == true)
                respawn.CambiaPos(); //Reubicar al jugador
            Debug.Log("Respawn");
       
    }
}
