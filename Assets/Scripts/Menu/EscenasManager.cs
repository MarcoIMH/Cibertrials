using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenasManager : MonoBehaviour
{
    public static EscenasManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Metodo para cargar las escenas
    /// </summary>
    /// <param name="escena"></param>
    public void CargarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    /// <summary>
    /// Metodo al que se llama desde el boton salir para cerrar el juego
    /// </summary>
    public void CerrarJuego()
    {
        Application.Quit();
    }
}
