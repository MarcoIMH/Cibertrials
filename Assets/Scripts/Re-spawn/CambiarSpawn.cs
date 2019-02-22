using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSpawn : MonoBehaviour {
    public Transform puntoSpawn;            //Referencia al punto de spawn puesta desde el editor
    
   void OnTriggerEnter2D(Collider2D other)
   {
        if (other.gameObject.CompareTag("jugador1") || other.gameObject.CompareTag("jugador1"))        //Comprobamos que quien colisiona es el jugador
        {
            puntoSpawn.position = other.transform.position;             //El punto de spawn se coloca en la posicion del jugador en ese momento y se queda ahí
            
            Debug.Log("Cambio de Spawn");
        } 
    }
   
}
