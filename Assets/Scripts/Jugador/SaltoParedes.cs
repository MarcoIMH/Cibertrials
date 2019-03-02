using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* MAÑANA PONGO LOS COMENTARIOS Y EXPLICO EL CODIGO. SON LAS 1.33 AM Y LLEVO HORA Y PICO INTENTANDO SOLUCIONAR UN BUG MUY CURIOSO QUE HAY EN CONTROLESJUGADOR
 * PERO NO HE SIDO CAPAZ, MAÑANA OS INFORMO */

public class SaltoParedes : MonoBehaviour {
    public float fuerzaSalto, x, y;
    Muros pared;
    Rigidbody2D rb;
    ControladorJugador controlador;
    Vector2 direccion;    
    float gravedadPorDefecto;
    bool puedeSaltar = false;

    // Use this for initialization
    void Start()
    {
        controlador = GetComponent<ControladorJugador>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("SetGravedadPorDefecto", 0.2f);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W) && puedeSaltar)
        {
            puedeSaltar = false;
            controlador.enabled = false;
            SaltoPared();                
        }        
    }

    public void SetSalto(bool puede, Muros lado)
    {     
        if(puede) rb.gravityScale = 0.1f;
        else rb.gravityScale = gravedadPorDefecto;

        pared = lado;
        puedeSaltar = puede;
    }    

    public void SaltoPared()
    {
        if (pared == Muros.izquierda) direccion = new Vector2(x, y);
        else direccion = new Vector2(-x, y);
        rb.AddForce(direccion * fuerzaSalto, ForceMode2D.Impulse);
        Invoke("RestauraControles", 1f);
    }   

    void RestauraControles()
    {
        controlador.enabled = true;        
    }

    void SetGravedadPorDefecto()
    {
        gravedadPorDefecto = rb.gravityScale;
    }
}
