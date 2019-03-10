using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirObjetivo : MonoBehaviour {

    public Transform posicionInicial;                                                                                    //Inicializar posición inicial con la primera posición de la patrulla
    public SpriteRenderer spriteVision;
    public float velocidadEnemigo, tiempoSeguir;                                                                        //tiempoSeguir= tiempo que el enemigo estará persiguiendo al objetivo

    SpriteRenderer sr;
    Transform jugador;    
    bool seguirJugador = false, regresaPosicionInicial=false;                                                          //Variables para controlar si queremos que esté de patrulla o "de regreso" a la posición inicial
    float r, g, b;                                                                                                      //Variables para almacenar el color del sprite de visión.

    // Use this for initialization
    void Start () {
        if (GetComponent<SpriteRenderer>() != null) sr = GetComponent<SpriteRenderer>();                                //Inicializamos el sprite para controlar el flip
                                                                                                                        //Inicializamos la posiciónInicial en la posición del enemigo, este será su punto de regreso   
        r = spriteVision.color.r;                                                                                       //Guardamos los colores del sprite de visión para poder indicar al jugador cuando lo ha detectado
        g = spriteVision.color.g;
        b = spriteVision.color.b;
    }

    // Update is called once per frame
    void Update() {
            
	}

    private void FixedUpdate()
    {
        if (seguirJugador) Patrulla(jugador);                                                                         

        if (regresaPosicionInicial)                                                                                     //Si esta activo dirigimos al enemigo a la posición inicial
        {          
            Patrulla(posicionInicial);                                                                                  //Le indicamos que patrulle hasta la posición inicial
            if (transform.position == posicionInicial.position)                                                        //Cuando llegue a la posición inicial cancelamos el regreso e inicializamos el movimiento de patrulla
            {
                regresaPosicionInicial = false;
                if(GetComponent<MovimientoEnemigos>()!=null)
                    GetComponent<MovimientoEnemigos>().SetMueveEnemigo(true);
            }
        }
    }

    /// <summary>
    /// Método para preguntar por el estado del seguimiento, de esta forma evitaremos que se reasigne un nuevo objetivo hasta que no haya terminado el seguimiento iniciado
    /// </summary>
    public bool PuedeIniciarSeguimiento()
    {
        if (!seguirJugador && !regresaPosicionInicial) return true;
        else return false;
    }

    /// <summary>
    /// Método para invocar el seguimiento del enemigo a la posición del jugador, este efecto durará el tiempo asignado -> "tiempoSeguir"
    /// Desautoriza la patrulla del enemigo para inicializar este movimiento, al finalizar el seguimiento la vuelve a autorizar. Este método se autoriza desde VisionVigilante
    /// </summary>
    /// <param name="posicion">Posición del jugador al que queremos que siga</param>
    public void SigueAlJugador(Transform posicion)
    {
        MovimientoEnemigos movEnemigo = GetComponent<MovimientoEnemigos>();                                         //Paramos el movimiento de patrulla normal entre puntos del enemigo
        movEnemigo.SetMueveEnemigo(false);
        jugador = posicion;                                                                                        //Establecemos nuevo objetivo a seguir
        seguirJugador = true;                                                                                      //Activamos patrulla        
        CambiaColorSpriteVision(true);
        Invoke("CancelaSeguimiento", tiempoSeguir);
    }

    /// <summary>
    /// Ejecuta el movimiento del enemigo desde su posición hacia el nuevo objetivo, a la velocidad asignada -> "velocidadEnemigo" y controla el flip de la imagen.
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

        transform.position = Vector3.MoveTowards(transform.position, nuevoObjetivo.position, velocidadEnemigo * Time.deltaTime);
    }

    /// <summary>
    /// Cancela el seguimiento e inicializa el regreso a la posición inicial. 
    /// </summary>
    void CancelaSeguimiento()
    {
        seguirJugador = false;
        regresaPosicionInicial = true;
        CambiaColorSpriteVision(false);                                                                             //Devolvemos el color del sprite a su versión original para indicar al jugador que ya no le sigue
    }

    /// <summary>
    /// Método para gestionar el cambio de color del sprite de visión. Esto ayudará al jugador a reconocer si lo han detectado o no
    /// </summary>
    /// <param name="cambia"></param>
    void CambiaColorSpriteVision(bool cambia)
    {
        if (cambia) spriteVision.color = Color.red;
        else spriteVision.color = new Color(r, g, b);
    }
}
