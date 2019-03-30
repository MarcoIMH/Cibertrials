using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraPico : MonoBehaviour {

    public float segundos = 5f; //tiempo que dura el powerUp
	  public int dañoModificado; //daño que aumentamos


	void Start ()
    {

  	}

	void Update ()
    {

  	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
         RomperParedes romper = collision.GetComponent<RomperParedes>();

         if (romper != null)
         {
            GameManager.instance.EjecutarSonido("PowerUp", 1);
            romper.IncreaseDamage(dañoModificado, segundos);
            Destroy(this.gameObject);
           // StartCoroutine(Pickup(romper));
         }
     }

    /// <summary>
    /// Aumentamos el daño que causamos al muro
    /// Desactivamos el collider y el spriteRenderer del powerUp
    /// Esperamos un tiempo
    /// Reducimos el daño que causamos al muro
    /// Destruimos el powerUp
    /// </summary>
    /// <param name="romper">variable del tipo DestroyWalls que usamos para llamar a los metodos
    /// que aumentan y reducen el daño causado al muro</param>
    /// <returns></returns>
    /*
    IEnumerator Pickup(DestroyWalls romper) //Corrutina
    {
        romper.AumentaDaño(aumentoDaño);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(segundos);

        romper.ReduceDamage(reduceDaño);

        GameObject.Destroy(this.gameObject);
    }
    */
}
