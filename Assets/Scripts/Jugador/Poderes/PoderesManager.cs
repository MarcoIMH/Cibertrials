﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PoderesManager : MonoBehaviour {
    
    public GameObject jugadorContrario, mundoContrario, muro, neblina, cuboHielo;         //GO para gestionar el jugador contrario. Prefabs de poderes.
    public Player jugador;    
    public int gemasMax;
    public float segundosInversionControles, tiempoNeblina;

    GameObject nieblaAux;
    Transform[] coordsPoderesMapa;            //Vector para almacenar todas las coordenadas de poderes del mapa que se cargue en escena
    PerdidasControl pcJC;                           //Perdidas de control del jugador contrario
    ControladorJugador controlesJugadorContrario;   //Controles del jugador contrario
    AudioSource audioSourceJC;
    KeyCode teclaPoder;

    //Poderes[] poder = new Poderes[4]; //array de poderes
    Poderes[] poder = { Poderes.inversionControles, Poderes.cubito , Poderes.muro, Poderes.neblina}; //array de poderes
    Poderes poderUsar;

    int gemas;
    bool habilidadActiva;

   	void Start () {
        gemas = 0;
        poderUsar = Poderes.sinPoder;
        habilidadActiva = false;   

        if (jugadorContrario.gameObject.GetComponent<AudioSource>() != null) audioSourceJC = jugadorContrario.gameObject.GetComponent<AudioSource>();

        if (jugadorContrario.gameObject.GetComponent<PerdidasControl>() != null) pcJC = jugadorContrario.gameObject.GetComponent<PerdidasControl>();

        if (jugadorContrario.gameObject.GetComponent<ControladorJugador>() != null)
        {
            controlesJugadorContrario = jugadorContrario.gameObject.GetComponent<ControladorJugador>();
        }
       
        Invoke("ConfiguraCoordenadasPoderes", 1f);                              //Invocamos la carga de coordenadas del mapa un segundo después  como seguridad 
    }                                                                           //para que de tiempo a todo a situarse en su lugar


    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(teclaPoder) && habilidadActiva) 
        {
            habilidadActiva = false;           
            switch (poderUsar)
            {
                case Poderes.inversionControles:
                    ActivaInvierteControles();
                    break;
                case Poderes.cubito:
                    AplicarCuboDeHielo();
                    break;
                case Poderes.muro:
                    ActivaMuro(); 
                    break;
                case Poderes.neblina:
                    ActivaNeblina();
                    break;
            }
            ReseteaGemas();
        }
	}

    /// <summary>
    /// Resetea las gemas cuando se usa el poder e informa al gameManager 
    /// </summary>
    public void ReseteaGemas()
    {
        gemas = 0;
        poderUsar = Poderes.sinPoder;
        GameManager.instance.ActualizaGemas(gemas, jugador, poderUsar);
    }

    /// <summary>
    /// añade una gema si puede. En caso de tenerlas todas, busca una habilidad
    /// </summary>
    /// <returns></returns>
    public bool AñadirGemas()
    {
        if (gemas < gemasMax)
        {
            gemas++;
            if (gemas == gemasMax)
            {
                BuscaHabilidad();
                habilidadActiva = true;
            }
            GameManager.instance.ActualizaGemas(gemas, jugador, poderUsar);
            return true;
        }
        else return false;             
    } 

    /// <summary>
    /// //activa el cubo de hielo y congela al jugador poniendo el estado de los controles a false
    /// </summary>
    public void AplicarCuboDeHielo()
    {
        GameManager.instance.EjecutarSonido(audioSourceJC, "CuboHielo");

        //pone el estado de los controles a false
        pcJC.DesactivaControles(-1, -1);
        //instancia el cubo de hielo entrando su script en ejecucion 
        GameObject newCuboHielo = Instantiate<GameObject>(cuboHielo, jugadorContrario.transform);
    }


    /// <summary>
    /// reactiva los controles
    /// </summary>
    public void DesactivarCuboDeHielo()
    {
        GetComponent<PerdidasControl>().ActivaControles();
    }

    /// <summary>
    /// Invierte la velocidad en X e intercambia las teclas de rodar y saltar
    /// </summary>
    public void ActivaInvierteControles()
    {
        GameManager.instance.EjecutarSonido(audioSourceJC, "IControles");

        //hace dichos cambios
        controlesJugadorContrario.ModificaVelocidad(-1);
        controlesJugadorContrario.ModificaVelocidadEstandar(-1);
        controlesJugadorContrario.SwapTeclas();
        //los revierte pasados "segundos" segundos
        Invoke("DesactivaInvierteControles", segundosInversionControles);

        jugadorContrario.GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(6, true);
    }

    /// <summary>
    /// Invierte la velocidad en X e intercambia las teclas de rodar y saltar
    /// Hace lo mismo que el metodo de activar pero si no el invoke entraria en bucle
    /// </summary>
    public void DesactivaInvierteControles()
    {
        controlesJugadorContrario.ModificaVelocidad(-1);
        controlesJugadorContrario.ModificaVelocidadEstandar(-1);
        controlesJugadorContrario.SwapTeclas();

        jugadorContrario.GetComponent<FeedbackVisual>().ActivarDesactivarFeedBack(6, false);
    }    

    public void SetTeclaPoder(KeyCode nuevaTecla)
    {
        teclaPoder = nuevaTecla;
    }

    /// <summary>
    /// asigna un poder al jugador
    /// </summary>
    void BuscaHabilidad()
    {
        poderUsar = poder[Random.Range(0,4)];                                                                                         
    }

    /// <summary>
    /// Método para configurar las coordenadas de los poderes/mapa, solicita al GameManager el nombre de la escena y llama al método para cargar coordenadas de ese mapa
    /// </summary>
    void ConfiguraCoordenadasPoderes()
    {
        coordsPoderesMapa = GameManager.instance.GetCoordenadasPoderes();    
    }

    /// <summary>
    /// Método para activar el muro, checkea la posición del jugador contrario e instancia el muro en la siguiente posición permitida cuando el poder sea activado                       
    /// </summary>
    void ActivaMuro()
    {
        int bandera = 0;    //Bandera para recorrer el array de coordenadas de poderes en mapa
        CalculaPosicionPoder(ref bandera);
        //Instanciamos el muro en la siguiente coordenada al jugador contrario
        GameObject muroNuevo;
        Vector3 pos;
        if (jugador == Player.jugador1)
            pos = new Vector3(coordsPoderesMapa[bandera].position.x, coordsPoderesMapa[bandera].position.y + muro.GetComponent<BoxCollider2D>().size.y / 2, 0);
        else pos = new Vector3(coordsPoderesMapa[bandera].localPosition.x, coordsPoderesMapa[bandera].localPosition.y + muro.GetComponent<BoxCollider2D>().size.y / 2, 0);
        GameManager.instance.EjecutarSonido(audioSourceJC, "PonerMuro");
        muroNuevo = Instantiate(muro, pos , Quaternion.identity, mundoContrario.transform);
        muroNuevo.layer = LayerMask.NameToLayer("Muro");
    }

    /// <summary>
    /// Método para activar el muro, checkea la posición del jugador contrario e instancia la neblina en la siguiente posición permitida cuando el poder sea activado   
    /// </summary>
    void ActivaNeblina()
    {
        int bandera = 0;    //Bandera para recorrer el array de coordenadas de poderes en mapa
        CalculaPosicionPoder(ref bandera);
        //Calculamos el ancho que tendrá la Neblina, dependerá de la distancia entre dos coordenadas sucesivas
        float ancho = coordsPoderesMapa[bandera + 1].position.x - coordsPoderesMapa[bandera].position.x;
        //Instanciamos la neblina en la siguiente coordenada al jugador contrario    
        GameObject niebla;

        GameManager.instance.EjecutarSonido(audioSourceJC, "Neblina");

        if (jugador == Player.jugador1)
            niebla = Instantiate(neblina, new Vector3(coordsPoderesMapa[bandera].position.x, coordsPoderesMapa[bandera].position.y, 0), Quaternion.identity, mundoContrario.transform);
        else
            niebla = Instantiate(neblina, new Vector3(coordsPoderesMapa[bandera].localPosition.x, coordsPoderesMapa[bandera].localPosition.y, 0), Quaternion.identity, mundoContrario.transform);

        niebla.transform.localScale = new Vector3(ancho, 40f, 0f);      //Configuramos su tamaño
        niebla.layer = LayerMask.NameToLayer("Neblina");                //Acomodamos la Neblina en su layer     
        nieblaAux = niebla;
        Debug.Log(niebla.transform.position.y + " vs " + niebla.transform.localPosition.y);
        Invoke("DesactivaNeblina", tiempoNeblina);        
    }

    /// <summary>
    /// destruye la neblina que se haya instanciado
    /// </summary>
    void DesactivaNeblina()
    {
        Destroy(nieblaAux);
    }

    /// <summary>
    /// Método para calcular la posición en la que se lanzará el poder Muro o Neblina. Al salir del método bandera apuntará a la coordenada de lanzamiento del poder
    /// </summary>
    /// <param name="bandera"></param>
    void CalculaPosicionPoder(ref int bandera)
    {
        Transform transformJC;
        if (jugadorContrario.GetComponent<Transform>() != null)
        {
            transformJC = jugadorContrario.GetComponent<Transform>();   //Si el JC está activo guarda su transform para gestionar conocer su posición
            //Mientras que la coordenada del jugador contrario sea menor que la del muro en esa posición sigue avanzando
            while (bandera + 1 < coordsPoderesMapa.Length && transformJC.position.x >= coordsPoderesMapa[bandera].position.x)    bandera++;
        }
    }
}
