using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlantaCerrada : MonoBehaviour {
    /// <summary>
    /// Este componente asociado al capullo cerrado de la planta controla si el objeto está activo o no segun lo dicte el
    /// script PlantaCarnivora
    /// </summary>
	public void ActivarObjeto()
    {
        gameObject.SetActive(true);
    }

    public void DesactivarObjeto()
    {
        gameObject.SetActive(false);
    }
}
