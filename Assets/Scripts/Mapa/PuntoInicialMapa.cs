using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoInicialMapa : MonoBehaviour
{
    GameObject goMundo;

    // Use this for initialization
    void Start()
    {
        goMundo = transform.parent.parent.gameObject;
        SetPuntoInicialMundo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Método para configurar el punto inicial de cada mapa. 
    /// Identifica al mundo e informa a GameManager de la posición inicial de cada mapa/mundo.
    /// </summary>
    void SetPuntoInicialMundo()
    {
        if (goMundo != null && goMundo.tag == "mundo1") GameManager.instance.SetPuntoInicial(transform, Mundos.mundoJ1);
        else if (goMundo != null) GameManager.instance.SetPuntoInicial(transform, Mundos.mundoJ2);
        Destroy(this);      //Destruimos el componente tras la carga ya que no es necesario para nada más.
    }
}