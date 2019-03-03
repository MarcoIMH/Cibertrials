using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaStun : MonoBehaviour {

    public int segundos;
    

    /// <summary>
    /// Este metodo se llama si el enemigo goomba colisiona por trigger con algun otro objeto
    /// </summary>
    /// /// <param name="other"></param> El que ha colisionado con el enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        PerdidasControl pc = other.GetComponent<PerdidasControl>();
        if (pc != null)
        {
           
            pc.DesactivaControles(segundos);
        }

    }
}
