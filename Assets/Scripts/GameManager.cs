using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mapPrefab, menuIngame;    
    public Dropdown resolucionesDropDown;
    public Image pantallaDeCarga;
    public MenuControles menuControles;

    Dictionary<string, KeyCode> controlesJ1 = new Dictionary<string, KeyCode>();
    Dictionary<string, KeyCode> controlesJ2 = new Dictionary<string, KeyCode>();

    Resolution[] resoluciones;
    UIManager UI;    
    AudioManager audioManager;
    GameObject mundoJ1, mundoJ2;
    Transform[] coordPoderesMapa;
    Transform transformJ1, transformJ2, puntoInicialJ1, puntoInicialJ2;
    KeyCode teclaMenuJ1, teclaMenuJ2;

    //variables que indican el numero de rondas ganadas por cada jugador
    int rondasJugador1, rondasJugador2;
    float volumen=1;

    string mapa="Mapa1";

    bool hayGanador=false, enMenu=false;

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

        teclaMenuJ1 = KeyCode.Escape;   //<----- CAMBIAR ASIGNACIÓN AL TERMINAR
        resoluciones = Screen.resolutions;
        ConfiguraDropDownResoluciones();

        PantallaDeCarga(1.5f);

        Invoke("CargaMapaEnMundos", 0.05f);     //Preguntar a Guille sobre como podría hacer y ordenar el Script Execution Order para no necesitar estos invokes.
        Invoke("ColocaJugadores", 0.07f);       //De ser así se podrían quitar y hacer que la carga fuera "limpia" al iniciar la ejecución.
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(teclaMenuJ1) && !enMenu) || (Input.GetKeyDown(teclaMenuJ2) && !enMenu))
        {
            if (Input.GetKeyDown(teclaMenuJ1)) menuControles.AbreMenuconfiguracion(controlesJ1, Player.jugador1);
            else menuControles.AbreMenuconfiguracion(controlesJ2, Player.jugador2);
            
            PausaJuego();
        }
        else if((Input.GetKeyDown(teclaMenuJ1) && enMenu) || (Input.GetKeyDown(teclaMenuJ2) && enMenu))
        {
            QuitaPausaJuego();
        }
    }

    public void SetTeclaMenu(KeyCode nuevaTecla, Player jugador)
    {
        if (jugador == Player.jugador1) teclaMenuJ1 = nuevaTecla;
        else teclaMenuJ2 = nuevaTecla;
    }

    public void SetControlesJugador(Dictionary<string, KeyCode> controlesJugador, Player jugador)
    {
        if (jugador == Player.jugador1) controlesJ1 = controlesJugador;
        else controlesJ2 = controlesJugador;
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
    /// Método para controlar el juego en pantalla completa o no. Trabaja con el Toogle de MenuConfiguracionIngame (interfaz Unity). SE PUEDE USAR PARA EL MENÚ OUTGAME.
    /// </summary>
    /// <param name="pantallaCompleta"></param>
    public void SetPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    /// <summary>
    /// Método para controlar los gráficos del juego. Trabaja con el DropDown de MenuConfiguracionIngame (interfaz Unity). SE PUEDE USAR PARA EL MENÚ OUTGAME TAMBIÉN.
    /// </summary>
    /// <param name="indiceGraficos"></param>
    public void SetNivelGraficos(int indiceGraficos)
    {
        QualitySettings.SetQualityLevel(indiceGraficos);
    }

    /// <summary>
    /// Configura el tamaño de la resolución de pantalla. Trabaja con el DropDown de MenuConfiguracionIngame (interfaz Unity). SE PUEDE USAR PARA EL MENÚ OUTGAME TAMBIÉN.
    /// </summary>
    public void SetResolucion(int indiceResolucion)
    {
        Screen.SetResolution(resoluciones[indiceResolucion].width, resoluciones[indiceResolucion].height, Screen.fullScreen);
    }

    /// <summary>
    /// Método para gestionar el volúmen de los sonidos. Trabaja con el slider de MenuConfiguracionIngame (interfaz UnitY). SE PUEDE USAR PARA EL MENÚ OUTGAME TAMBIÉN.
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumen(float vol)
    {
        volumen = vol;
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
        audioManager.EjecutarSonido(audioSource, nombreSonido, volumen);
    }

    public void EjecutarSonido(string nombreSonido, int eleccion)
    {
        audioManager.EjecutarSonido(nombreSonido, eleccion, volumen);
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
        if (mapPrefab != null)
        {
            var mapInstanceJ1 = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            mapInstanceJ1.transform.parent = mundoJ1.transform;

            var mapInstanceJ2 = Instantiate(mapPrefab, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
            mapInstanceJ2.transform.parent = mundoJ2.transform;
        }        
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

        if(transformJ1.gameObject.GetComponent<ControladorJugador>()!=null)
            transformJ1.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(true);
        if (transformJ2.gameObject.GetComponent<ControladorJugador>() != null)
            transformJ2.gameObject.GetComponent<ControladorJugador>().SetEstadoControlador(true);
    }    

    /// <summary>
    /// Configura el juego para establecer una pausa y muestra la pantalla de menu inGame, se activa en caso de que algún jugador abre dicho menú.
    /// </summary>
    void PausaJuego()
    {
        enMenu = true;
        Time.timeScale = 0;

        if (transformJ1.gameObject.GetComponent<ControladorJugador>() != null)
            InterruptorMundos(mundoJ1, transformJ1.gameObject.GetComponent<ControladorJugador>(), false);

        if (transformJ2.gameObject.GetComponent<ControladorJugador>() != null)
            InterruptorMundos(mundoJ2, transformJ2.gameObject.GetComponent<ControladorJugador>(), false);

        UI.gameObject.SetActive(false);
        menuIngame.SetActive(true);
    }

    /// <summary>
    /// Restablece el juego de su pausa y cierra el menú.
    /// </summary>
    void QuitaPausaJuego()
    {
        enMenu = false;
        Time.timeScale = 1;

        if (transformJ1.gameObject.GetComponent<ControladorJugador>() != null)
            InterruptorMundos(mundoJ1, transformJ1.gameObject.GetComponent<ControladorJugador>(), true);

        if (transformJ2.gameObject.GetComponent<ControladorJugador>() != null)
            InterruptorMundos(mundoJ2, transformJ2.gameObject.GetComponent<ControladorJugador>(), true);
        
        menuIngame.SetActive(false);
        UI.gameObject.SetActive(true);
    }

    /// <summary>
    /// Método para controlar la visualización y activación de mundo/jugador. 
    /// Hace de "interruptor" para cada mundo y su jugador.
    /// </summary>
    /// <param name="mundo">Mundo</param>
    /// <param name="cont">Controles del jugador en dicho mundo</param>
    /// <param name="est">Valor del interruptor</param>
    void InterruptorMundos(GameObject mundo, ControladorJugador cont, bool est)
    {
        if (cont != null) cont.SetEstadoControlador(est);
        mundo.SetActive(est);
    }

    /// <summary>
    /// Método para automatizar el paso de las distintas resoluciones existentes al dropdown de resoluciones.
    /// </summary>
    void ConfiguraDropDownResoluciones()
    {
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();

        int resolucionActual = 0;
        for(int x = 0; x < resoluciones.Length; x++)
        {
            string opcion = resoluciones[x].width + " x " + resoluciones[x].height;
            opciones.Add(opcion);

            if (resoluciones[x].width == Screen.currentResolution.width &&
                resoluciones[x].height == Screen.currentResolution.height)
                resolucionActual = x;
        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();
    }

    /// <summary>
    /// Activa la pantalla de carga y la desactiva al rato
    /// </summary>
    /// <param name="tiempo">tiempo que tarda en desactivarse</param>
    void PantallaDeCarga(float tiempo)
    {
        pantallaDeCarga.gameObject.SetActive(true);
        Invoke("QuitarPantallaDeCarga", tiempo);
    }

    /// <summary>
    /// Desactiva la pantalla de carga
    /// </summary>
    void QuitarPantallaDeCarga()
    {
        pantallaDeCarga.gameObject.SetActive(false);
    }
}
