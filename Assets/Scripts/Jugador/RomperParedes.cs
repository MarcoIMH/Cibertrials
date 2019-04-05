﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomperParedes : MonoBehaviour {

    public int daño = 1;
    public float distancia = 1f; // distancia a partir de la cual se puede empezar a picar el muro    

    AudioSource audioSource;
    KeyCode teclaRomperParedes;      //letra con la que usamos el pico
    Animator anim;

    int capa;
    int auxDaño; // guarda el daño original

	 void Start ()
     {
        capa = LayerMask.GetMask("Muro");
        auxDaño = daño;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
	 }

	 void Update ()
     {
        RaycastHit2D contacto = Physics2D.Raycast(transform.position, transform.right, distancia, capa);

        if (contacto.collider != null) // si colisionamos con algo
        {
            Pared pared = contacto.collider.gameObject.GetComponent<Pared>();

            if (pared != null && Input.GetKeyDown(teclaRomperParedes))//si ese algo es el componente Wall
            {
                GameManager.instance.EjecutarSonido(audioSource, "Picar");
                pared.DañarPared(daño); //dañamos al muro
            }
        }

        /*DESCOMENTAR PARA ANIMACIONES
       //animacion de picar
       if (Input.GetKeyDown(teclaRomperParedes))
       {
           anim.SetTrigger("Picar");
       }
       */
     }

    /// <summary>
    /// aumentamos el daño del pico
    /// </summary>
    /// <param name="cantidad">cantidad de daño que aumentamos al pico</param>
    public void IncreaseDamage(int cantidad, float segundos)
    {
        daño += cantidad;
        Invoke("ReduceDamage", segundos);
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(2, true);
    }

    /// <summary>
    /// reducimos el daño del pico
    /// </summary>
    /// <param name="cantidad">cantidad de daño que reducimos al pico</param>
    public void ReduceDamage()
    {
        daño = auxDaño;
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(2, false);
    }

    public KeyCode GetTeclaRomperParedes()
    {
        return teclaRomperParedes;
    }

    public void SetTeclaRomperParedes(KeyCode nuevaTecla)
    {
        teclaRomperParedes = nuevaTecla;
    }
}
