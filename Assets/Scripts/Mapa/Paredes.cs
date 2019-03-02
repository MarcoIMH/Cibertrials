using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paredes : MonoBehaviour {
    public Transform paredContraria;
    Muros ladoPared;

    /* MAÑANA PONGO LOS COMENTARIOS Y EXPLICO EL CODIGO. SON LAS 1.33 AM Y LLEVO HORA Y PICO INTENTANDO SOLUCIONAR UN BUG MUY CURIOSO QUE HAY EN CONTROLESJUGADOR
 * PERO NO HE SIDO CAPAZ, MAÑANA OS INFORMO */

    // Use this for initialization
    void Start () {
        if (transform.position.x < paredContraria.position.x)
            ladoPared = Muros.izquierda;
        else ladoPared = Muros.derecha;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Rigidbody2D>()!=null)
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (other.gameObject.GetComponent<SaltoParedes>() != null)
            other.gameObject.GetComponent<SaltoParedes>().SetSalto(true, ladoPared);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<SaltoParedes>() != null)
            other.gameObject.GetComponent<SaltoParedes>().SetSalto(false, ladoPared);
    }
}
