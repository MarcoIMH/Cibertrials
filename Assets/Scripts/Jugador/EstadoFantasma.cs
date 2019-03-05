using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoFantasma : MonoBehaviour {

    bool estadoFantasma;

    /// <summary>
    /// metodo que se llama desde el objeto con el power up
    /// </summary>
    /// <param name="segundos"></param>
    public void ActivaEstadoFantasma(float segundos)
    {
        estadoFantasma = true;
        // a los 'segundos' se desactiva la variable
        Invoke("DesactivaEstadoFantasma", segundos);
    }

    void DesactivaEstadoFantasma()
    {
        estadoFantasma = false;
    }

    /// <summary>
    /// Sirve para que los enemigos accedan a la variable estadoFantasma
    /// </summary>
    /// <returns></returns>
    public bool CogerEstadoFantasma()
    {
        return estadoFantasma;
    }
}
