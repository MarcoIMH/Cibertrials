using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonSalir : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CambiarAEscenaMenu(string nombre)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nombre);
    }
}
