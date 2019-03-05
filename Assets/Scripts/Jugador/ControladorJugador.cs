using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour {
    public KeyCode teclaRodar, teclaSaltar;
    public int alturaSalto, distanciaRecorrida;
    public float VelocidadRodar, velocidadX; //cantidad de velocidad reducida en % (de 0 a 1)                    
    public string axisHorizontal, axisVertical;   
    
    BoxCollider2D colliderCorre;
    CircleCollider2D colliderRueda;
    Rigidbody2D rb;
    float deltaX, g, velocidadY, velocidadEstandar;       //velocidadEstandar = variable auxiliar donde guardamos la velocidad original
    bool salto, estadoControles = true, rodando, empezarRodar, pararRodar, puedeSaltar, 
         entreParedes=false;  //vemos si el jugador se encuentra entre paredes(mientras que rueda)

    // Use this for initialization
    void Start()
    {
        g = (-2 * alturaSalto * velocidadX * velocidadX) / ((distanciaRecorrida / 2) * (distanciaRecorrida / 2));   //gravedad calculada
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = g / Physics2D.gravity.y;                                                                  //gravedad del jugador
        velocidadY = (2*alturaSalto*velocidadX)/(distanciaRecorrida/2);                                             //velocidad del salto
        
        colliderCorre = GetComponent<BoxCollider2D>();
        colliderRueda = GetComponent<CircleCollider2D>();

        velocidadEstandar = velocidadX;
        
        salto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoControles)
        {
            //RODAR
            //tecla con la que se pulsa para rodar
            //ponemos GetKey para que sea mientras esta se mantiene
            empezarRodar = Input.GetKeyDown(teclaRodar); 
            pararRodar = Input.GetKeyUp(teclaRodar);

            if (colliderCorre != null && colliderRueda != null)
            {
                //si pulsamos tecla
                if (empezarRodar)
                {
                    rodando = true;
                    colliderCorre.enabled = false;
                    colliderRueda.enabled = true;
                    CambiosPerdidaControl(PerdidaControles.ralentizar, VelocidadRodar, true);
                }
                else if (pararRodar && !entreParedes)//revisar si esto es necesario
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

            deltaX = Input.GetAxis(axisHorizontal);

            //SALTO

            if (Input.GetKey(teclaSaltar) && puedeSaltar)
            {
                salto = true;
                puedeSaltar = false;
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

        // USAR ADDFORCE 
        //salto en pared

        //mov horizontal   
        rb.velocity = new Vector2(deltaX * velocidadX, rb.velocity.y);
    }

    /// <summary>
    /// Metodo de eleccion de perdida de control
    /// </summary>
    /// <param name="caso"> Tipo de CC </param>
    /// <param name="cambioVelocidad"> Porcentaje a modificar de la velocida en X (escrito como 0,...)</param>
    /// <param name="estado"> si estado=false personaje stun</param>
    public void CambiosPerdidaControl (PerdidaControles caso, float cambioVelocidad, bool estado)
    {
        switch(caso)
        {
          case PerdidaControles.ralentizar:
            velocidadX = cambioVelocidad * velocidadEstandar;
            break;
          case PerdidaControles.stun:
                if (!estado) ReseteaStats();
                Debug.Log(estado);
                estadoControles = estado;
                break;           
        }
    }

    public void CambiosPoderes(Poderes caso)
    {
        switch (caso)
        {
            case Poderes.cambioControles:
                //codigo cambio controles
                break;
            case Poderes.cubito:
                //codigo cubito
                break;
            case Poderes.muro:
                //codigo muro 
                break;
            case Poderes.neblina:
                //codigo neblina
                break;
        }
    }


    /// <summary>
    /// Cambia el estado de ver si esta o no entre paredes
    /// </summary>
    /// <param name="check"></param>
    public void CheckDejarRodar(bool check)
    {
        entreParedes = check;
    }

    
    /// <summary>
    /// Aumenta la velocidadX durante un tiempo y luego se vuelve a su valor original
    /// </summary>
    /// <param name="cantidad">cantidad de velocidad que aumentamos a velocidadX</param>
    /// <param name="duracion">tiempo que dura el aumento</param>
    public void AumentaVelocidad(float cantidad,float duracion)
    {
        velocidadX += cantidad;
        Invoke("RestauraVelocidad", duracion);
    }

    /// <summary>
    /// Velocidad en X vuelve a su estado original
    /// </summary>
    public void RestauraVelocidad()
    {
        velocidadX = velocidadEstandar;
    }  

    void ReseteaStats()
    {
        deltaX = 0;
        //deltaY = 0;
        rb.velocity = Vector3.zero;
    }

    /// <summary>
    /// Metodo que se llama desde checksalto cuando el jugador toca el suelo
    /// </summary>
    public void ActivaPuedeSaltar()
    {
        puedeSaltar = true;
    }     
}
