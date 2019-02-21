using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPico : MonoBehaviour {

    public float seconds = 5f; //tiempo que dura el powerUp
	public int damageToAdd =2; //daño que aumentamos
    private int reduceDamage; //daño que reducimos
   
	void Start ()
    {
        reduceDamage = damageToAdd;
	}
		
	void Update ()
    {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {                
         DestroyWalls destroy = collision.GetComponent<DestroyWalls>();

         if (destroy)
         {
            StartCoroutine(Pickup(destroy));
         }                      
    }

    /// <summary>
    /// Aumentamos el daño que causamos al muro
    /// Desactivamos el collider y el spriteRenderer del powerUp
    /// Esperamos un tiempo
    /// Reducimos el daño que causamos al muro
    /// Destruimos el powerUp
    /// </summary>
    /// <param name="destroy">variable del tipo DestroyWalls que usamos para llamar a los metodos
    /// que aumentan y reducen el daño causado al muro</param>
    /// <returns></returns>
    IEnumerator Pickup(DestroyWalls destroy) //Corrutina
    {       
        destroy.IncreaseDamage(damageToAdd); 
    
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(seconds); 

        destroy.ReduceDamage(reduceDamage); 
       
        GameObject.Destroy(this.gameObject); 
    }
}
