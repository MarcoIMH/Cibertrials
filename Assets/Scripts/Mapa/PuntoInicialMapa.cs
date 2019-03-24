using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoInicialMapa : MonoBehaviour {
    GameObject goMundo;

	// Use this for initialization
	void Start ()
    {
        goMundo = transform.parent.parent.gameObject;
        SetPuntoInicialMundo();
    }
    
	// Update is called once per frame
	void Update () {
		
	}

    void SetPuntoInicialMundo()
    {
        if(goMundo!=null && goMundo.tag == "mundo1") GameManager.instance.SetPuntoInicial(this.transform, Mundos.mundoJ1);
        else if(goMundo!=null) GameManager.instance.SetPuntoInicial(this.transform, Mundos.mundoJ2);
    }
}
