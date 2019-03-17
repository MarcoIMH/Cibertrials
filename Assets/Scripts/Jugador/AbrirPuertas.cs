using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    bool tieneLlave = false;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Cuando el jugador colisiona con la llave ponemos tieneLlave a true y destruimos la llave
        Llave llave = collision.gameObject.GetComponent<Llave>();

        if (llave != null)
        {
            tieneLlave = llave.CogeLlave();
            Destroy(llave.gameObject);
        }
    }

    /// <summary>
    /// Devuelve el valor de la variable tieneLlave
    /// </summary>
    /// <returns></returns>
    public bool TieneLlave()
    {
        return tieneLlave;
    }

    /// <summary>
    /// Pone la variable tieneLlave a false
    /// </summary>
    public void QuitarLlave()
    {
        tieneLlave = false;
    }

}
