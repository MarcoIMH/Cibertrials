using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWalls : MonoBehaviour {

    public int damage = 1;
    public float distance = 1f; // distancia a partir de la cual se puede empezar a picar el muro
    public GameObject origin; //objeto a partir del cual sale disparado el rayo
    public KeyCode key;      //letra con la que usamos el pico


	void Start ()
    {
       
	}
	
    
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin.gameObject.transform.position, origin.gameObject.transform.right, distance);

        
        if (hit.collider != null) // si colisionamos con algo
        {          
            Wall wall = hit.collider.gameObject.GetComponent<Wall>();

            if (wall != null && Input.GetKeyDown(key))//si ese algo es el componente Wall
            {
                wall.DamageWall(damage); //dañamos al muro            
            }
        }  
	}   
    
    /// <summary>
    /// aumentamos el daño del pico
    /// </summary>
    /// <param name="amount">cantidad de daño que aumentamos al pico</param>
    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }

    /// <summary>
    /// reducimos el daño del pico
    /// </summary>
    /// <param name="amount">cantidad de daño que reducimos al pico</param>
    public void ReduceDamage(int amount)
    {
        damage -= amount;
    }    
}
