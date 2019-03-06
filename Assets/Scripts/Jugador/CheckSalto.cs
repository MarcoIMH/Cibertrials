using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSalto : MonoBehaviour
{
    ControladorJugador controladorJugador;

    // Use this for initialization
    void Start()
    {
        controladorJugador = GetComponentInParent<ControladorJugador>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controladorJugador != null)
        {
            controladorJugador.ActivaPuedeSaltar();
            controladorJugador.EstaEnPared(false);
        }
    }
}
