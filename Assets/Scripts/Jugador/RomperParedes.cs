using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomperParedes : MonoBehaviour {

    public int daño = 1;
    public float distancia = 1f; // distancia a partir de la cual se puede empezar a picar el muro
    public KeyCode tecla;      //letra con la que usamos el pico

    int capa;
    int auxDaño; // guarda el daño original

	 void Start ()
     {
        capa = LayerMask.GetMask("Muro");
        auxDaño = daño;
	 }

	 void Update ()
     {
        RaycastHit2D contacto = Physics2D.Raycast(transform.position, transform.right, distancia, capa);

        if (contacto.collider != null) // si colisionamos con algo
        {
            Pared pared = contacto.collider.gameObject.GetComponent<Pared>();

            if (pared != null && Input.GetKeyDown(tecla))//si ese algo es el componente Wall
            {
                pared.DañarPared(daño); //dañamos al muro
            }
        }
  	 }

    /// <summary>
    /// aumentamos el daño del pico
    /// </summary>
    /// <param name="cantidad">cantidad de daño que aumentamos al pico</param>
    public void IncreaseDamage(int cantidad, float segundos)
    {
        daño += cantidad;
        Invoke("ReduceDamage", segundos);
          //AÑADIR AQUI EL CAMBIO DE COLOR DEL SPRITE
    }

    /// <summary>
    /// reducimos el daño del pico
    /// </summary>
    /// <param name="cantidad">cantidad de daño que reducimos al pico</param>
    public void ReduceDamage()
    {
        daño = auxDaño;
        //AÑADIR AQUI PARA QUITAR EL COLOR DEL SPRITE
    }
    public KeyCode DarTeclaRomper()
    {
        return tecla;
    }
    
}
