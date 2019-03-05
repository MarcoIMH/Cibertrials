using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionVigilante : MonoBehaviour {
    public float reduccionVelVigilante;
    public float maxAngulo, minAngulo, varAngulo;

    Vector3 vectorRotacion;

    // Use this for initialization
    void Start ()
    {
        vectorRotacion  = new Vector3 (0f, 0f, varAngulo);
    }
	
	// Update is called once per frame
	void Update ()
    {        
        transform.Rotate(vectorRotacion);       

        if(transform.rotation.z >= maxAngulo || transform.rotation.z <= minAngulo)
        {
            vectorRotacion *= (-1);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se usa en el if para ver si el jugador tiene el estado fantasma o no
        EstadoFantasma est = other.GetComponent<EstadoFantasma>();

        /* PerdidasControl pc = other.GetComponent<PerdidasControl>();
         if (pc != null)
         {

             pc.ActivaModificaVelocidad(reduccionVelVigilante);
         }*/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Lineas de testing para "SeguirObjevito.cs"   -> Comento la pérdida de control para que se ejecute esto en su lugar. Habría que progamar aquí en "qué modo" está la visión del enemigo (stun, freeze, seguir, etc)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////     
        if (this.gameObject.GetComponentInParent<SeguirObjetivo>() != null && est != null)
        {
            SeguirObjetivo so = this.gameObject.GetComponentInParent<SeguirObjetivo>();
            if (!est.CogerEstadoFantasma() && so.PuedeIniciarNuevaPatrulla()) so.PatrullaHaciaPosicion(other.transform);
        }
    }
}
