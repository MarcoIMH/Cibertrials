using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    public KeyCode teclaRodar, teclaSaltar;
    public int alturaSalto, distanciaRecorrida;
    public float VelocidadRodar, velocidadX; //cantidad de velocidad reducida en % (de 0 a 1)
    public string axisHorizontal, axisVertical;

    BoxCollider2D colliderCorre;
    CircleCollider2D colliderRueda;
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator anim;
    float deltaX, g, velocidadY, velocidadEstandar;       //velocidadEstandar = variable auxiliar donde guardamos la velocidad original
    bool salto, estadoControles = true, rodando, puedeSaltar,
         enTubería = false, enPared = false, movHorizontal = false;

    // Use this for initialization
    void Start()
    {
        g = (-2 * alturaSalto * velocidadX * velocidadX) / ((distanciaRecorrida / 2) * (distanciaRecorrida / 2));   //gravedad calculada
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = g / Physics2D.gravity.y;                                                                  //gravedad del jugador
        velocidadY = (2 * alturaSalto * velocidadX) / (distanciaRecorrida / 2);                                             //velocidad del salto

        colliderCorre = GetComponent<BoxCollider2D>();
        colliderRueda = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        velocidadEstandar = velocidadX;

        salto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoControles)
        {
            //RODAR
            if (colliderCorre != null && colliderRueda != null)
            {
                //si pulsamos tecla
                if (Input.GetKeyDown(teclaRodar))
                {
                    rodando = true;
                    colliderCorre.enabled = false;
                    colliderRueda.enabled = true;
                    ModificaVelocidad(VelocidadRodar);
                }
                else if (Input.GetKeyUp(teclaRodar) && !enTubería)
                {
                    rodando = false;
                    colliderCorre.enabled = true;
                    colliderRueda.enabled = false;
                    velocidadX = velocidadEstandar;

                    // AnimacionCorrer (anim.correr)
                }

                if (rodando)
                {
                    // AnimacionRodar (anim.rodar)
                }
            }



            //flip del personaje
            if(Input.GetAxis(axisHorizontal) > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if(Input.GetAxis(axisHorizontal) < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            //animacion de moverse
            bool parado = (Input.GetAxis(axisHorizontal) == 0);
            anim.SetBool("Moviendose", !parado);

            //mov horizontal
            if (Input.GetAxis(axisHorizontal) != 0 && !enPared)
            {
                deltaX = Input.GetAxis(axisHorizontal);
                movHorizontal = true;

                //sonido de moverse
                if (!audioSource.isPlaying && puedeSaltar)
                {
                    audioSource.loop = true;
                    GameManager.instance.EjecutarSonido(audioSource, "Moverse");
                }
            }
            else
            {
                audioSource.loop = false; //se pone el loop a false para que el sonido no se ejecute tras pararse
                movHorizontal = false;
            }

            //salto
            if (Input.GetKey(teclaSaltar) && puedeSaltar)
            {
                salto = true;
                puedeSaltar = false;
                audioSource.loop = false;
                GameManager.instance.EjecutarSonido(audioSource, "Salto");
            }
        }
    }


    private void FixedUpdate()
    {
        //salto
        if (salto)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocidadY);
            salto = false;
        }
        //mov horizontal
        if (movHorizontal && !enPared)
            rb.velocity = new Vector2(deltaX * velocidadX, rb.velocity.y);
    }

    public void SetTeclaRodar(KeyCode nuevaTecla)
    {
        teclaRodar = nuevaTecla;
    }

    public void SetTeclaSaltar(KeyCode nuevaTecla)
    {
        teclaSaltar = nuevaTecla;
    }

    /// <summary>
    /// Setter para gestionar el estado funcional del controlador.
    /// </summary>
    public void SetEstadoControlador(bool est)
    {
        estadoControles = est;
    }

    /// <summary>
    /// Metodo que se llama desde checksalto cuando el jugador toca el suelo
    /// </summary>
    public void ActivaPuedeSaltar()
    {
        puedeSaltar = true;
    }

    /// <summary>
    /// Método que se utiliza para comunicar al controlador si está en pared o no
    /// </summary>
    /// <param name="estado"></param>
    public void EstaEnPared(bool estado)
    {
        enPared = estado;
    }

    /// <summary>
    /// Cambia el estado de ver si esta o no entre paredes
    /// </summary>
    /// <param name="check"></param>
    public void CheckDejarRodar(bool check)
    {
        enTubería = check;
    }

    /// <summary>
    /// Pone al jugador de pie trás pasar una tubería si este no esta presionando
    /// la tecla de rodar
    /// </summary>
    public void PonerDePie()
    {
        if (!Input.GetKey(teclaRodar)) //si no presiona la tecla rodar
        {
            //Restauramos al jugador como si corriera
            rodando = false;
            colliderCorre.enabled = true;
            colliderRueda.enabled = false;
            RestauraVelocidad();
        }
    }

    /// <summary>
    /// Gestión de la velocidad del avatar
    /// </summary>
    /// <param name="cambioVelocidad">multiplicador de velocidad</param>
    public void ModificaVelocidad(float cambioVelocidad)
    {
        velocidadX = cambioVelocidad * velocidadEstandar;
    }

    /// <summary>
    /// Aumenta la velocidadX durante un tiempo e invoca a RestauraVelocidad tras el tiempo "duracion".
    /// </summary>
    /// <param name="cantidad">cantidad de velocidad que aumentamos a velocidadX</param>
    /// <param name="duracion">tiempo que dura el aumento</param>
    public void AumentaVelocidad(float cantidad, float duracion)
    {
        velocidadX += cantidad;
        Invoke("RestauraVelocidad", duracion);
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(5, true);
    }

    /// <summary>
    /// Velocidad en X vuelve a su estado original
    /// </summary>
    public void RestauraVelocidad()
    {
        velocidadX = velocidadEstandar;
        GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(5, false);
    }

    /// <summary>
    /// Método para resetear valores de movimiento y pone el loop del AS a false para
    /// que no se ejecute el sonido de caminar tras morir
    /// </summary>
    public void ReseteaStats()
    {
        deltaX = 0;
        //deltaY = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        audioSource.loop = false;
    }

    /// <summary>
    /// Método para cambiar el input de las teclas
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public void SwapTeclas()
    {
        KeyCode aux;
        aux = teclaRodar;
        teclaRodar = teclaSaltar;
        teclaSaltar = aux;
    }
}
