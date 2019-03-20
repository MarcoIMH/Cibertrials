using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Declaracion de ambos jugadores
    Player jugador1, jugador2;

    //variables que indican el numero de rondas ganadas por cada jugador
    int rondasJugador1, rondasJugador2;

    UIManager UI;


    //Asegurarse de que solo hay una instancia
    public static GameManager instance = null;
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
        //Ambos jugadores comienzan la partida con 0 rondas ganadas
        rondasJugador1 = rondasJugador2 = 0;


    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// este método se llama cuando uno de los jugadores alcanza la llegada (arrival)
    /// </summary>
    /// <param name="ganador"></param>
    public void FinalizarRonda(Player ganador)
    {
        if (ganador == jugador1)
        {
            rondasJugador1++;
        }
        else rondasJugador2++;

        //-----------> aqui habría que desactivar el componente "playercontroller" de ambos jugadores
    }

    public void ActualizaGemas(int gema, Player jugador)
    {
        UI.ActualizaGema(gema, jugador);
    }

    
    /// <summary>
    /// Llama al metodo ActualizarLlave del UIManager
    /// </summary>
    /// <param name="jugador"></param>
    /// <param name="activado"></param>
    public void ActualizarLlave(Player jugador, bool activado)
    {
        UI.ActualizarLlave(jugador, activado);
    }
    

    /// <summary>
    /// Recogemos UI
    /// </summary>
    /// <param name="UIM"></param>
    public void SetUI(UIManager UIM)
    {
        UI = UIM;
    }

    /// <summary>
    /// Método para informar de la escena que se está cargando. TERMINAR EL CÓDIGO CUANDO ESTÉN TODAS LAS ESCENAS Y EL CAMBIO DE ESCENA HECHO
    /// </summary>
    /// <returns></returns>
    public string NombreEscena()
    {
        return "Mapa3";
    }
}
