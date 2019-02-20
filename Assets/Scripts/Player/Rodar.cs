using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodar : MonoBehaviour {

    public GameObject colliderRun, colliderRoll; //dos GameObject con dos collider que referencian al de correr y al de rodar

    public float velocity, //velocidad del player en horizontal
                 reduceVelocity; //cantidad de velocidad reducida en %

    float auxVelocity;  //variable auxiliar donde guardamos la velocidad original
    bool roll;

	void Start () {

        auxVelocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {

        //tecla con la que se pulsa para rodar
        //ponemos GetKey para que sea mientras esta se mantiene
        //En caso de meter animaciones intercalas entre correr y rodar habrá que meter GetKeyDown y GetKeyUp
        roll = Input.GetKey(KeyCode.Space); //---->Hay que mirar los axis y ver si esta o no bien


        //si pulsamos tecla
        if (roll)
        {
            colliderRun.SetActive(false);
            colliderRoll.SetActive(true);
            ChangeVelocity(reduceVelocity);
        }
        else //revisar si esto es necesario
        {
            colliderRun.SetActive(true);
            colliderRoll.SetActive(false);
            velocity = auxVelocity;
        }


    }
    
 
    /// <summary>
    ///  Sirve para modificar la velocidad del player
    /// </summary>
    /// <param name="changeVelocity"> Porcentaje por el que será modificada la velocidad </param>
    public void ChangeVelocity(float changeVelocity)
    {
        velocity = changeVelocity*auxVelocity;
    }
}
