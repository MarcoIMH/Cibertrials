using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubitoHielo : MonoBehaviour {

    public int GolpesParaRomper;

    int GolpesRecibidos;

    RomperParedes picar;
    PerdidasControl pc;

	// Use this for initialization
	void Start ()
    {
        GolpesRecibidos = 0;

        if (this.gameObject.GetComponentInParent<RomperParedes>() != null)
        {
            picar = this.gameObject.GetComponentInParent<RomperParedes>();
        }

        if (this.gameObject.GetComponentInParent<PerdidasControl>() != null)
        {
            pc = this.gameObject.GetComponentInParent<PerdidasControl>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G)/*picar.tecla*/)
        {
            GolpesRecibidos++;
        }
        if (GolpesRecibidos >= GolpesParaRomper)
        {
            pc.DesactivarCuboDeHielo();
            Destroy(this.gameObject);
        }
	}
}
