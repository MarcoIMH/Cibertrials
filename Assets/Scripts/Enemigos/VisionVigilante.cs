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
        PerdidasControl pc = other.GetComponent<PerdidasControl>();
        if (pc != null)
        {
            
            pc.ActivaModificaVelocidad(reduccionVelVigilante);
        }
        
    }
}
