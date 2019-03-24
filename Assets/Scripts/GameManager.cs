using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mapPrefab;
    UIManager UI;
    GameObject mundoJ1, mundoJ2;        
    Transform transformJ1, transformJ2, puntoInicialJ1, puntoInicialJ2;    

    //variables que indican el numero de rondas ganadas por cada jugador
    int rondasJugador1, rondasJugador2;

    string mapa;

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

        Invoke("CargaMapaEnMundos", 0.1f);
        Invoke("ColocaJugadores", 0.2f);
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
        if (ganador == Player.jugador1)
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
    /// Getter para informar del mapa que se está gestionando al resto de componentes.
    /// </summary>
    /// <returns></returns>
    public string GetNombreEscena()
    {
        return mapa;
    }

    /// <summary>
    /// Setter para configurar el mapa que se va a gestionar. LLAMAR A ESTE MÉTODO CUANDO SE HAGA EL DE CARGAR ESCENA
    /// </summary>
    public void SetNombreEscena(string map)
    {
        mapa = map;
    }

    /// <summary>
    /// Setter para que el controlador PuntoInicial pueda informar del punto inicial de cada mapa para cada jugador
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="jug"></param>
    public void SetPuntoInicial(Transform tr, Mundos mundo)
    {
        if (mundo == Mundos.mundoJ1) puntoInicialJ1 = tr;
        else puntoInicialJ2 = tr;
    }

    public void SetMundoYJugador(Mundos mundo, GameObject goMundo, Transform tr)
    {//Cambiar a bool y adaptar ControlMundos cuando implemente una forma de comprobar que realmente se ha cargado todo -> pensar detenidamente
        if (mundo == Mundos.mundoJ1)
        {
            mundoJ1 = goMundo;
            transformJ1 = tr;
        }
        else
        {
            mundoJ2 = goMundo;
            transformJ2 = tr;
        }
    }

    void CargaMapaEnMundos()
    {
        GameObject mapInstance = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        mapInstance.transform.parent = mundoJ1.transform;

        GameObject mapInstance2 = Instantiate(mapPrefab, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;        
        mapInstance2.transform.parent = mundoJ2.transform;
    }
    
    void ColocaJugadores()
    {
        transformJ1.position = puntoInicialJ1.position;
        transformJ2.position = puntoInicialJ2.position;
    }
}
