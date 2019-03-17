using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraTroncos : MonoBehaviour {

    //periodo de tiempo entre generacion y generacion
    public float periodo;
    //referencia al prefab
    public GameObject tronco;

    Transform tran;
    // Use this for initialization
    void Start () {

        tran = transform;
        //Genera 1 tronco cada periodo segundos
        InvokeRepeating("GeneraTronco", 1, periodo);        
	}
	/// <summary>
    /// hace un Instantiate del tronco en la posicion del objeto generador
    /// </summary>
    void GeneraTronco()
    {
        GameObject newTronco = Instantiate(tronco, tran);
    }
}
