using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour {
    public KeyCode teclaRodar;
    public int alturaSalto, distanciaRecorrida;
    public float VelocidadRodar, velocidadX; //cantidad de velocidad reducida en % (de 0 a 1)                    
    public string axisHorizontal, axisVertical;
   
    
    BoxCollider2D colliderCorre;
    CircleCollider2D colliderRueda;
    Rigidbody2D rb;
    float deltaX, deltaY, g, velocidadY, velocidadEstandar;                                                         //velocidadEstandar = variable auxiliar donde guardamos la velocidad original
    bool salto, estadoControles = true, estadoReduceVelocidad = false, rodando, empezarRodar, pararRodar, puedeSaltar;  

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
            //En caso de meter animaciones intercalas entre correr y rodar habrá que meter GetKeyDown y GetKeyUp
            empezarRodar = Input.GetKeyDown(teclaRodar); //----> Hay que mirar los axis y ver si esta o no bien
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
                else if (pararRodar)//revisar si esto es necesario
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

            //SALTO
            if (Input.GetAxis(axisVertical) > 0 && puedeSaltar)
            {
                deltaY = 1;
                salto = true;
                puedeSaltar = false;
            }
            
            deltaX = Input.GetAxis(axisHorizontal);            
        }        
    }

    private void FixedUpdate()
    {
        //salto
        if (salto)
        {
            rb.velocity = new Vector2(rb.velocity.x, deltaY * velocidadY);
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

    public void RestauraVelocidad()
    {
        velocidadX =  velocidadEstandar;
    }  


    void ReseteaStats()
    {
        deltaX = 0;
        deltaY = 0;
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
