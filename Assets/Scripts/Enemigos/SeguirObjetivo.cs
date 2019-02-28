using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirObjetivo : MonoBehaviour {

    public Transform posicionInicial;                                                                                    //Inicializar posición inicial con la primera posición de la patrulla
    public SpriteRenderer spriteVision;
    public float velocidadEnemigo, tiempoSeguir;                                                                        //tiempoSeguir= tiempo que el enemigo estará persiguiendo al objetivo

    SpriteRenderer sr;
    Transform objetivo;    
    bool estadoPatrulla = false, regresaPosicionInicial=false;                                                          //Variables para controlar si queremos que esté de patrulla o "de regreso" a la posición inicial
    float r, g, b;

    // Use this for initialization
    void Start () {
        if (GetComponent<SpriteRenderer>() != null) sr = GetComponent<SpriteRenderer>();                                //Inicializamos el sprite para controlar el flip
                                                                                                                        //Inicializamos la posiciónInicial en la posición del enemigo, este será su punto de regreso   
        r = spriteVision.color.r;
        g = spriteVision.color.g;
        b = spriteVision.color.b;
    }

    // Update is called once per frame
    void Update() {
            
	}

    private void FixedUpdate()
    {
        if (estadoPatrulla) Patrulla(objetivo);

        if (regresaPosicionInicial)                                                                                     //Si esta activo dirigimos al enemigo a la posición inicial
        {          
            Patrulla(posicionInicial);
            if (transform.position == posicionInicial.position)                                                        //Cuando llegue a la posición inicial cancelamos el regreso e inicializamos el movimiento de patrulla
            {
                regresaPosicionInicial = false;
                MovimientoEnemigos movEnemigo = GetComponent<MovimientoEnemigos>();
                movEnemigo.SetMueveEnemigo(true);
            }
        }
    }   

    /// <summary>
    /// Ejecuta el movimiento del enemigo hacia el nuevo objetivo a la velocidad asignada -> "velocidadEnemigo" y controla el flip de la imagen
    /// </summary>
    /// <param name="nuevoObjetivo">Posición del objetivo al que seguir</param>
    void Patrulla(Transform nuevoObjetivo)
    {
        if (nuevoObjetivo.position.x < transform.position.x)
        {
            sr.flipX = true;
            spriteVision.flipX = true;
        }
        else
        {
            sr.flipX = false;
            spriteVision.flipX = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, nuevoObjetivo.position, velocidadEnemigo*Time.deltaTime);
    }

    /// <summary>
    /// Cancela la patrulla e inicializa el regreso a la posición inicial
    /// </summary>
    void CancelaPatrulla()
    {
        estadoPatrulla = false;
        regresaPosicionInicial = true;
        CambiaColorSpriteVision(false);
    }

    void CambiaColorSpriteVision(bool cambia)
    {
        if(cambia) spriteVision.color = Color.red;
        else spriteVision.color = new Color(r,g,b);
    }

    /// <summary>
    /// Método para invocar el seguimiento del enemigo a la posición indicada, este efecto durará el tiempo asignado -> "tiempoSeguir"
    /// Para la patrulla del enemigo para inicializar este movimiento, al finalizar la vuelve a activar.
    /// </summary>
    /// <param name="posicion">Posición del objetivo al que queremos que siga</param>
    public void PatrullaHaciaPosicion(Transform posicion)
    {
        MovimientoEnemigos movEnemigo = GetComponent<MovimientoEnemigos>();                                         //Paramos el movimiento de patrulla del enemigo
        movEnemigo.SetMueveEnemigo(false);
        objetivo = posicion;                                                                                        //Establecemos nuevo objetivo a seguir
        estadoPatrulla = true;                                                                                      //Activamos patrulla        
        CambiaColorSpriteVision(true);
        Invoke("CancelaPatrulla", tiempoSeguir);
    }

    /// <summary>
    /// Método para preguntar por el estado de la patrulla, de esta forma evitaremos que se reasigne un nuevo objetivo hasta que no haya terminado la patrulla iniciada
    /// </summary>
    public bool PuedeIniciarNuevaPatrulla()
    {
        if (!estadoPatrulla && !regresaPosicionInicial) return true;
        else return false;
    }
}
