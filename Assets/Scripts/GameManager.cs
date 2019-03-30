using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mapPrefab;
    UIManager UI;
    AudioManager audioManager;
    GameObject mundoJ1, mundoJ2;
    Transform[] coordPoderesMapa;
    Transform transformJ1, transformJ2, puntoInicialJ1, puntoInicialJ2;

    //variables que indican el numero de rondas ganadas por cada jugador
    int rondasJugador1, rondasJugador2;

    string mapa="Mapa1";

    bool hayGanador=false;

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

        Invoke("CargaMapaEnMundos", 0.05f);     //Preguntar a Guille sobre como podría hacer y ordenar el Script Execution Order para no necesitar estos invokes.
        Invoke("ColocaJugadores", 0.07f);       //De ser así se podrían quitar y hacer que la carga fuera "limpia" al iniciar la ejecución.
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
        if (!hayGanador)
        {
            if (ganador == Player.jugador1)
            {
                rondasJugador1++;
            }
            else rondasJugador2++;
            hayGanador = true;
        }
    }

    public void ActualizaGemas(int gema, Player jugador, Poderes poder)
    {
        UI.ActualizaGema(gema, jugador, poder);
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
    /// Recogemos el audio manager
    /// </summary>
    /// <param name="AM"></param>
    public void SetAudioManager(AudioManager AM)
    {
        audioManager = AM;
    }

    public void EjecutarSonido(AudioSource audioSource, string nombreSonido)
    {
        audioManager.EjecutarSonido(audioSource, nombreSonido);
    }

    public void EjecutarSonido(string nombreSonido, int eleccion)
    {
        audioManager.EjecutarSonido(nombreSonido, eleccion);
    }

    /// <summary>
    /// Getter para informar del mapa que se está gestionando a los componentes que lo necesiten.
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
    /// Método para informar al GameManager de las coordenadas que se usarán para la gestión de los poderes.
    /// </summary>
    /// <param name="coords"></param>
    public void SetCoordenadasPoderes(Transform[] coords)
    {
        coordPoderesMapa = coords;
    }

    /// <summary>
    /// Método para informar a los componentes que lo necesiten de las coordenadas que se usarán para los poderes.
    /// </summary>
    /// <returns></returns>
    public Transform[] GetCoordenadasPoderes()
    {
        return coordPoderesMapa;
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

    /// <summary>
    /// Método para tomar control en GameManager de cada mundo y su jugador.
    /// </summary>
    /// <param name="mundo">Tipo de Mundo(enum) para identificar la referencia de cada mundo y jugador</param>
    /// <param name="goMundo">Referencia al GO del mundo en cuestión</param>
    /// <param name="tr">Transform del jugador correspondiente a ese mundo</param>
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

    /// <summary>
    /// Método para instanciar mapa "reseteado", colocarlo, y asignarlo a su mundo correspondiente.
    /// </summary>
    void CargaMapaEnMundos()
    {
        var mapInstanceJ1 = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        mapInstanceJ1.transform.parent = mundoJ1.transform;

        var mapInstanceJ2 = Instantiate(mapPrefab, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
        mapInstanceJ2.transform.parent = mundoJ2.transform;
    }

    /// <summary>
    /// Coloca los jugadores en sus puntos iniciales correspondientes en cada mapa y los hace visible para los jugadores y activa sus controles.
    /// </summary>
    void ColocaJugadores()
    {
        transformJ1.position = puntoInicialJ1.position;
        transformJ2.position = puntoInicialJ2.position;

        transformJ1.gameObject.SetActive(true);
        transformJ2.gameObject.SetActive(true);

        transformJ1.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(true);
        transformJ2.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(true);
    }
}
