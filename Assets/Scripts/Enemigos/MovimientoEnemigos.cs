using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigos : MonoBehaviour {    
    public Transform[] posiciones;
    public float velocidad, magnitud, frecuencia;
    int bandera=1;                                                                                  //Inicializamos bandera en 1 para que haga el movimiento posicion0 -> posicion1 y continue 
                                                                                                    //a partir de ahí. Importante inicializar posicion enemigo en posicion 0!!

    private void Start()
    {
        if (posiciones != null && posiciones.Length > 1)                                            //Comprobamos que no sea nulo y que tenga mínimo dos puntos
        {
            
            bool check = true;                                                                      //Comprobamos que ninguna posición sea null
            for (int x = 0; x < posiciones.Length;x++)
                if (posiciones[x] == null) check = false;                                           //Si alguna posición es nula cambiamos el valor de check para no inicializar la posición del enemigo
            if (check) transform.position = posiciones[0].position;                                 //Si se cumple todo inicializamos la posición del enemigo en la posición 0
        }

        
        if (GetComponent<Rigidbody2D>() != null)    GetComponent<Rigidbody2D>().gravityScale = 0;   //Nos aseguramos de que no le afecte la gravedad
    }

    private void Update()
    {
        if(transform.position == posiciones[bandera].position)                                      //Si la posición del enemigo alcanza la posición del siguiente al que se dirige 
        {                                                                                           // apunta el vector a la siguiente posicion y autoriza el cambio de ruta
            int posicionAnterior = bandera;
            bandera++;
            if (bandera == posiciones.Length) bandera = 0;                                          //Si la bandera llega al fin del vector la reseteamos     

            
            if (posiciones[bandera].position.x < posiciones[posicionAnterior].position.x)                //Comprobamos la coordenada x de la siguiente posición, si es menor cambia el flip
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else gameObject.GetComponent<SpriteRenderer>().flipX = false;                                            
        }
    }

    private void FixedUpdate()
    {
        PatrullaHastaPosicion(posiciones[bandera]);                                                  
    }

    /// <summary>
    /// Método para hacer que el enemigo se mueva en patrulla de unos puntos a otros. 
    /// Realiza a gusto del diseñador movimiento senoidal uniforme o rectilíneo uniforme entre el número de puntos deseados.
    /// En caso de desear un movimiento senoidal uniforme modificar el valor de las variables: magnitud, frecuencia (cada una imita su función física).
    /// </summary>
    /// <param name="posicion"></param>
    void PatrullaHastaPosicion(Transform posicion)                                                  
    {
        //Debug.Log("PosicionEnemigo(x,y):  "+ transform.position.x+","+ transform.position.y);
        //Debug.Log("PuntoPatrulla(x,y):  " + posicion.position.x + "," + posicion.position.y);
        transform.position = Vector3.MoveTowards(transform.position + transform.up * Mathf.Sin(Time.time * frecuencia) * magnitud, posicion.position, velocidad * Time.deltaTime);
    }
}
