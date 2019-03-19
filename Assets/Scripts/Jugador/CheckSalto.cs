using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSalto : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ControladorJugador controladorJugador = GetComponentInParent<ControladorJugador>();

        if (controladorJugador != null)
        {
            controladorJugador.ActivaPuedeSaltar();
            controladorJugador.EstaEnPared(false); //Invocación de seguridad para desactivar el salto en paredes de ControladorJugador.
        }
    }
}
