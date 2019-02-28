﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigos : MonoBehaviour {  
    
    public Transform[] posiciones;                                                                  //Instanciar y asignar tantas posiciones como se deseen a través de la interfaz de Unity.
    public SpriteRenderer spriteVision;
    public float velocidad, amplitud, frecuencia, tiempoPausa;                                      //tiempoPausa= el tiempo de pausa en caso de desear que el enemigo haga una pausa en cada posición de patrulla                                                         

    int bandera=1;                                                                                  //Inicializamos bandera en 1 para que haga el movimiento posicion0 -> posicion1 y continue 
    bool mueveEnemigo = true;                                                                       //a partir de ahí. Importante inicializar posicion enemigo en posicion 0!!  
    
    private void Start()
    {
        if (posiciones != null && posiciones.Length > 1)                                            //Comprobamos que no sea nulo y que tenga mínimo dos puntos
        {
            
            bool check = true;                                                                      //Comprobamos que ninguna posición sea null
            for (int x = 0; x < posiciones.Length;x++)
                if (posiciones[x] == null) check = false;                                           //Si alguna posición es nula cambiamos el valor de check para no inicializar la posición del enemigo
            if (check) transform.position = posiciones[0].position;                                 //Si se cumple todo nos aseguramos de inicializar la posición del enemigo en la posición 0
        }
                
        if (GetComponent<Rigidbody2D>() != null &&                                                  //Nos aseguramos de que no le afecte la gravedad en caso de que el enemigo sea dinámico.
            GetComponent<Rigidbody2D>().bodyType== RigidbodyType2D.Dynamic)
                GetComponent<Rigidbody2D>().gravityScale = 0;   
    }

    private void Update()
    {        
        if(mueveEnemigo && transform.position == posiciones[bandera].position)                      //Si la posición del enemigo alcanza la posición del punto al que se dirige 
        {                                                                                           // apuntamos bandera a la siguiente posición del vector
            if (tiempoPausa>0)
            {
                mueveEnemigo = false;
                Invoke("TerminarPausa", tiempoPausa);
            }
            int posicionAnterior = bandera;
            bandera++;
            if (bandera == posiciones.Length) bandera = 0;                                          //Si la bandera llega al fin del vector la reseteamos   
            
            CompruebaFlip(posicionAnterior);            
        }
    }

    private void FixedUpdate()
    {
        if(mueveEnemigo) PatrullaHastaPosicion(posiciones[bandera]);                                                  
    }

    void TerminarPausa()
    {
        mueveEnemigo = true;
    }

    void CompruebaFlip(int posicionAnterior)                                                          //Comprobamos la coordenada x de la siguiente posición, si es menor cambia el flip
    {
        if (posiciones[bandera].position.x < posiciones[posicionAnterior].position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if(spriteVision!=null) spriteVision.flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (spriteVision != null) spriteVision.flipX = false;
        }
    }

    /// <summary>
    /// Método para hacer que el enemigo se mueva en patrulla de unos puntos a otros. 
    /// Realiza a gusto del diseñador movimiento senoidal uniforme o rectilíneo uniforme entre el número de puntos deseados.
    /// En caso de desear un movimiento senoidal uniforme modificar el valor de las variables: amplitud, frecuencia (cada una imita su función física).
    /// </summary>
    /// <param name="posicion"></param>
    void PatrullaHastaPosicion(Transform posicion)                                                  
    {
        //Debug.Log("PosicionEnemigo(x,y):  "+ transform.position.x+","+ transform.position.y);
        //Debug.Log("PuntoPatrulla(x,y):  " + posicion.position.x + "," + posicion.position.y);
        transform.position = Vector3.MoveTowards(transform.position + transform.up * Mathf.Sin(Time.time * frecuencia) * amplitud, posicion.position, velocidad * Time.deltaTime);
    }    

    /// <summary>
    /// Configura si debe lanzarse o no el movimiento del enemigo.
    /// </summary>
    /// <param name="confirmacion"></param>
    public void SetMueveEnemigo(bool confirmacion)
    {
        mueveEnemigo = confirmacion;
        if (confirmacion)
        {
            bandera = 1;
            CompruebaFlip(bandera--);
        }        
    }
}
