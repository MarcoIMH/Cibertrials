using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour {

    public int alturaSalto;
    public string axisHorizontal, axisVertical;

    public float cantidadVelocidadReducida; //cantidad de velocidad reducida en %
    public KeyCode teclaRodar;

    public float velocidadX, fuerzaDeSalto, maxVelocidadX;

    float deltaX, deltaY;

    BoxCollider2D colliderCorre;
    CircleCollider2D colliderRueda;

    Rigidbody2D rb;
    bool salto, estadoControles = true, estadoReduceVelocidad = false;

    float maxVelocidadAux;  //variable auxiliar donde guardamos la velocidad original
    bool rodando, empezarRodar, pararRodar;

    // Use this for initialization
    void Start()
    {

        colliderCorre = GetComponent<BoxCollider2D>();
        colliderRueda = GetComponent<CircleCollider2D>();

        maxVelocidadAux = velocidadX;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = (2 * maxVelocidadX / 2 * maxVelocidadX / 2) / (alturaSalto * alturaSalto);
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
                    CambiaVelocidad(cantidadVelocidadReducida);
                }
                else if (pararRodar)//revisar si esto es necesario
                {
                    rodando = false;

                    colliderCorre.enabled = true;
                    colliderRueda.enabled = false;
                    velocidadX = maxVelocidadAux;

                    // AnimacionCorrer (anim.correr)
                }
                if (rodando)
                {
                    // AnimacionRodar (anim.rodar)
                }
            }


            //SALTO
            //RaycastHit2D enSuelo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.localScale.y/1.95f), -Vector2.up);
            //Debug.DrawRay(transform.position, transform.up);

            if (Input.GetAxis(axisVertical) > 0 && Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                deltaY = 1;
                salto = true;
            }
            if (Mathf.Abs(rb.velocity.x) < maxVelocidadX)
            {
                deltaX = Input.GetAxis(axisHorizontal);
            }
        }        
    }
    private void FixedUpdate()
    {
        //salto
        if (salto)
        {
            rb.velocity = new Vector2(deltaX * velocidadX, deltaY * fuerzaDeSalto);
            salto = false;
        }
      //  else
            //mov horizontal       
            rb.velocity = new Vector2(deltaX * velocidadX, rb.velocity.y);
    }

    /// <summary>
    ///  Sirve para modificar la velocidad del player
    /// </summary>
    /// <param name="cambioVelocidad"> Porcentaje por el que será modificada la velocidad </param>
    public void CambiaVelocidad(float cambioVelocidad)
    {
        velocidadX = cambioVelocidad * maxVelocidadAux;
    }  

    public void SetActivaControles(bool estado)
    {
        if (!estado) ReseteaStats();
        Debug.Log(estado);
        estadoControles = estado;
    }

    public void SetActivaReduceVelocidad(bool estado)
    {
        estadoReduceVelocidad = estado;
        if (estado)
        {
            maxVelocidadX = maxVelocidadX - cantidadVelocidadReducida;
        }
        else
        {
            maxVelocidadX = maxVelocidadAux;
        }
    }

    void ReseteaStats()
    {
        deltaX = 0;
        deltaY = 0;
        rb.velocity = Vector3.zero;
    }
}
