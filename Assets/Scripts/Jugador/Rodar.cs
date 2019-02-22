using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodar : MonoBehaviour {

    public GameObject colliderCorre, colliderRueda; //dos GameObject con dos collider que referencian al de correr y al de rodar

    public float velocidad, //velocidad del player en horizontal
                 reduceVelocidad; //cantidad de velocidad reducida en %

    float velocidadAux;  //variable auxiliar donde guardamos la velocidad original
    bool rodando;

	void Start () {

        velocidadAux = velocidad;
	}
	
	// Update is called once per frame
	void Update () {

        //tecla con la que se pulsa para rodar
        //ponemos GetKey para que sea mientras esta se mantiene
        //En caso de meter animaciones intercalas entre correr y rodar habrá que meter GetKeyDown y GetKeyUp
        rodando = Input.GetKey(KeyCode.Space); //---->Hay que mirar los axis y ver si esta o no bien


        //si pulsamos tecla
        if (rodando)
        {
            colliderCorre.SetActive(false);
            colliderRueda.SetActive(true);
            CambiaVelocidad(reduceVelocidad);
        }
        else //revisar si esto es necesario
        {
            colliderCorre.SetActive(true);
            colliderRueda.SetActive(false);
            velocidad = velocidadAux;
        }


    }
    
 
    /// <summary>
    ///  Sirve para modificar la velocidad del player
    /// </summary>
    /// <param name="cambioVelocidad"> Porcentaje por el que será modificada la velocidad </param>
    public void CambiaVelocidad(float cambioVelocidad)
    {
        velocidad = cambioVelocidad*velocidadAux;
    }
}
