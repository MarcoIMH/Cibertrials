using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaCarnivora : MonoBehaviour {

    public float retardo,reapertura;
    private ControlPlantaAbierta CapulloA;
    private ControlPlantaCerrada CapulloB;


    
    void Start () {
       CapulloA = GetComponentInChildren<ControlPlantaAbierta>();       //Componente del sprite de la con el capullo abierto
       CapulloB = GetComponentInChildren<ControlPlantaCerrada>();      //Componente del sprite de la con el capullo cerrado

        CapulloB.DesactivarObjeto();        //Como para poder localizarlo tiene que estar activo al encontrarlo lo desactivamos
    }
	
	
	void Update () {
		
	}
    /// <summary>
    /// Al entrar en colision con la base de la plata esta ejecuta dos metodos, uno que hará que la planta se cierre y otro que hará
    /// que despues se habra de nuevo. Ambos tiempos son configurables desde el editor
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal == Vector2.down)       //Si el golpe es por arriba si que permite la ejecución
        {
            Invoke("AtaquePlanta", retardo);
            Invoke("DeshacerAtaquePlanta", retardo + reapertura);
            //Como ambos metodos se ejecutan a la misma vez hay sumarle al retardo del segundo el del primero para que se mantenga la diferencias
        }
       
    }
    //Pone uno de los sprites desactivados y activa el otro
    void AtaquePlanta()
    {
        CapulloA.DesactivarObjeto();
        CapulloB.ActivarObjeto();
    }

    //Deshace el metodo de ataque planta, vuele a abrir la flor
    void DeshacerAtaquePlanta()
    {
        CapulloA.ActivarObjeto();
        CapulloB.DesactivarObjeto();
    }
}
