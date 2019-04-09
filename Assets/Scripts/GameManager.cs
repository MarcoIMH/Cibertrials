using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public GameObject mapPrefab, mapPrefab2, mapPrefab3, menuIngame;   
    public Image pantallaDeCarga;
    public MenuControles menuControles;

    struct Graficos
    {
        public bool pantallaCompleta;
        public int indiceGraficos, indiceResolucion;
        public Resolution resolucion;
    }
    Graficos configuracionGraficos;

    GameObject mapaJ1, mapaJ2;

    UIManager UI;    
    AudioManager audioManager;
    GameObject mundoJ1, mundoJ2;
    Transform[] coordPoderesMapa;
    Transform transformJ1, transformJ2, puntoInicialJ1, puntoInicialJ2;
    KeyCode teclaMenuJ1, teclaMenuJ2;
    
    int rondasJugador1, rondasJugador2;
    float volumenSonidos, volumenMusica;
    
    int indiceMapaActual = 1;

    bool j1EnMeta=false, j2EnMeta = false, enMenu=false;

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
        rondasJugador1 = 0;
        rondasJugador2 = 0;

        PantallaDeCarga(1.5f);

        volumenSonidos = 1;

        Invoke("CargaMapaEnMundos", 0.05f);     //Preguntar a Guille sobre como podría hacer y ordenar el Script Execution Order para no necesitar estos invokes.
        Invoke("ColocaJugadores", 0.5f);       //De ser así se podrían quitar y hacer que la carga fuera "limpia" al iniciar la ejecución.
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(teclaMenuJ1) && !enMenu) || (Input.GetKeyDown(teclaMenuJ2) && !enMenu))
        {
            if (Input.GetKeyDown(teclaMenuJ1)) menuControles.CargaMenuControles(Player.jugador1);
            else menuControles.CargaMenuControles(Player.jugador2);

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

    /// <summary>
    /// este método se llama cuando uno de los jugadores alcanza la llegada (arrival)
    /// </summary>
    /// <param name="jugadorEnMeta"></param>
    public void FinalizarRonda(Player jugadorEnMeta)
    {    
        if (!j1EnMeta && jugadorEnMeta == Player.jugador1)
        {
            if(!j2EnMeta)rondasJugador1++;
            j1EnMeta = true;
        }
        else
        {
            if (!j1EnMeta) rondasJugador2++;
            j2EnMeta = true;
        }

        if (j1EnMeta && j2EnMeta && indiceMapaActual < 3)
        {
            indiceMapaActual++;            
            pantallaDeCarga.gameObject.GetComponent<PantallaDeCarga>().MostrarResultados(rondasJugador1, rondasJugador2, indiceMapaActual, 8f);
            PantallaDeCarga(8f);
            CargaMapaEnMundos();
            Invoke("ColocaJugadores", 1f);
            j1EnMeta = false;
            j2EnMeta = false;           
        }

        if (j1EnMeta && j2EnMeta && indiceMapaActual == 3)
        {
            indiceMapaActual = 1;
            j1EnMeta = false;
            j2EnMeta = false;
            SceneManager.LoadScene("Menu");
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
    /// Método para almacenar el volúmen de los sonidos.  
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumenSonidos(float vol)
    {
        volumenSonidos = vol;
    }

    /// <summary>
    /// Método para almacenar el volúmen de la musica.  
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumenMusica(float vol)
    {
        volumenSonidos = vol;
    }

    public void GuardaConfiguracionGraficos(bool pantallaCompleta, int indiceGraficos, int indiceResolucion, Resolution resolucionActual)
    {
        configuracionGraficos.pantallaCompleta = pantallaCompleta;
        configuracionGraficos.indiceGraficos = indiceGraficos;
        configuracionGraficos.indiceResolucion = indiceResolucion;
        configuracionGraficos.resolucion = resolucionActual;
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
        audioManager.EjecutarSonido(audioSource, nombreSonido, volumenSonidos);
    }

    public void EjecutarSonido(string nombreSonido, int eleccion)
    {
        audioManager.EjecutarSonido(nombreSonido, eleccion, volumenSonidos);
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
        if (indiceMapaActual != 1)
        {
            Destroy(mapaJ1.gameObject);
            Destroy(mapaJ2.gameObject);
        }

        switch (indiceMapaActual)
        {
            case 1:
                if (mapPrefab != null)
                {
                    var mapa1 = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    mapa1.transform.parent = mundoJ1.transform;
                    mapaJ1 = mapa1;

                    var mapa2 = Instantiate(mapPrefab, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
                    mapa2.transform.parent = mundoJ2.transform;
                    mapaJ2 = mapa2;
                }break;
            case 2:
                if (mapPrefab2 != null)
                {
                    var mapa1 = Instantiate(mapPrefab2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    mapa1.transform.parent = mundoJ1.transform;
                    mapaJ1 = mapa1;

                    var mapa2 = Instantiate(mapPrefab2, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
                    mapa2.transform.parent = mundoJ2.transform;
                    mapaJ2 = mapa2;
                }
                break;
            case 3:
                if (mapPrefab3 != null){
                    var mapa1 = Instantiate(mapPrefab3, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    mapa1.transform.parent = mundoJ1.transform;
                    mapaJ1 = mapa1;

                    var mapa2 = Instantiate(mapPrefab3, new Vector3(0, -100, 0), Quaternion.identity) as GameObject;
                    mapa2.transform.parent = mundoJ2.transform;
                    mapaJ2 = mapa2;
                }
                break;
        }   
    }

    /// <summary>
    /// Coloca los jugadores en sus puntos iniciales correspondientes en cada mapa, los hace visible para los jugadores y activa sus controles.
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
    public void QuitaPausaJuego()
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
