using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PoderesManager : MonoBehaviour {
    
    public GameObject jugadorContrario, muro, neblina, cuboHielo, neb1,neb2;                                                                           //GO para gestionar el jugador contrario. Prefabs de muro y neblina para su instancia.
    public Player jugador;
    public KeyCode teclaPoder;
    public int gemasMax;
    public float segundosInversionControles, tiempoNeblina;
    

    PerdidasControl pc;
    ControladorJugador controlesJugadorContrario;

    
    struct Coordenadas                                                                                                           //Struct para gestionar las coordenadas del mapa
    {
        public float x;
        public float y;
    }
    Coordenadas[] coordsPoderesMapa;                                                                                             //Vector para almacenar todas las coordenadas de poderes del mapa que se cargue en escena
                                                                                         //Vector para almacenar todas las coordenadas de poderes del mapa que se cargue en escena
                                                                                         //CAMBIAR AL MÉTODO NORMAL DE INSTANCIA DE PODERES (NEBLINA/MURO)
    Poderes[] poder = new Poderes[4]; //array de poderes
    Poderes poderUsar;
    int gemas;
    bool habilidadActiva;

   	void Start () {
        gemas = 0;
        habilidadActiva = false;   

        //asignamos los poderes
        poder[0] = Poderes.cubito;
        poder[1] = Poderes.inversionControles;
        poder[2] = Poderes.muro;
        poder[3] = Poderes.neblina;

        if(jugadorContrario.gameObject.GetComponent<PerdidasControl>() != null) pc = jugadorContrario.gameObject.GetComponent<PerdidasControl>();

        if (jugadorContrario.gameObject.GetComponent<ControladorJugador>() != null)
        {
            controlesJugadorContrario = jugadorContrario.gameObject.GetComponent<ControladorJugador>();
        }

       
        Invoke("ConfiguraCoordenadasPoderes", 1f);                                                                               //Invocamos la carga de coordenadas del mapa un segundo después  como seguridad 
    }                                                                                                                            //para que de tiempo a todo a situarse en su lugar


    // Update is called once per frame
    void Update () {
        //print(habilidadActiva);
        if(Input.GetKeyDown(teclaPoder) && habilidadActiva) 
        {
            habilidadActiva = false;
            ReseteaGemas();
            switch (poderUsar)
            {
                case Poderes.inversionControles:
                    ActivaInvierteControles();
                    break;
                case Poderes.cubito:
                    AplicarCuboDeHielo();
                    break;
                case Poderes.muro:
                    //ActivaMuro();                                     //DEVOLVER EL MURO A SU LUGAR CUANDO SE PASE EL HITO
                    ActivaNeblina();
                    break;
                case Poderes.neblina:
                    ActivaNeblina();
                    break;
            }
        }
	}

    public void ReseteaGemas()
    {
        gemas = 0;
        GameManager.instance.ActualizaGemas(gemas, jugador);
    }

    public bool AñadirGemas()
    {
        if (gemas < gemasMax)
        {
            gemas++;
            GameManager.instance.ActualizaGemas(gemas, jugador);
            if (gemas == gemasMax)
            {
                BuscaHabilidad();
                habilidadActiva = true;
            }
            return true;
        }
        else return false;             
    } 

    /// <summary>
    /// //activa el cubode hielo y congela al jugador poniendo el estado de los controles a false
    /// </summary>
    public void AplicarCuboDeHielo()
    {
        //pone el estado de los controles a false
        controlesJugadorContrario.CongelarJugador(true);
        //instancia el cubo de hielo entrando su script en ejecucion 
        GameObject newCuboHielo = Instantiate<GameObject>(cuboHielo, jugadorContrario.transform);
    }


    /// <summary>
    /// reactiva los controles
    /// </summary>
    public void DesactivarCuboDeHielo()
    {
        GetComponent<ControladorJugador>().CongelarJugador(false);
        //controlesJugadorContrario.Checkcc(false);
    }

    /// <summary>
    /// Invierte la velocidad en X e intercambia las teclas de rodar y saltar
    /// </summary>
    public void ActivaInvierteControles()
    {
        //hace dichos cambios
        controlesJugadorContrario.CambiosPoderes(Poderes.inversionControles, false);
        //los revierte pasados "segundos" segundos
        Invoke("DesactivaInvierteControles", segundosInversionControles);
    }

    /// <summary>
    /// Invierte la velocidad en X e intercambia las teclas de rodar y saltar
    /// Hace lo mismo que el metodo de activar pero si no el invoke entraria en bucle
    /// </summary>
    public void DesactivaInvierteControles()
    {
        controlesJugadorContrario.CambiosPoderes(Poderes.inversionControles, false);
    }    


    void BuscaHabilidad()
    {
        poderUsar = poder[Random.Range(0,3)];                                                                                    //Lo pongo en 3,4 para forzar que use neblina. Por defecto es 0 4. 
                                                                                                                                 //Devolver a la normalidad cuando termine de hacer los poderes
    }

    /// <summary>
    /// Método para configurar las coordenadas de los poderes/mapa, solicita al GameManager el nombre de la escena y llama al método para cargar coordenadas de ese mapa
    /// </summary>
    void ConfiguraCoordenadasPoderes()
    {
        string mapa;
        //string mapa = GameManager.instance.NombreEscena();                                                                       //Preguntamos a GameManager por el nombre de la escena
        if (jugador == Player.jugador1)
            mapa = "J2";
        else
            mapa = "J1";
        LeeCordenadasPoderesDelMapa("Assets/Scripts/Mapa/MurosMapa/" + mapa + ".txt");                                               //Cargamos el archivo correspondiente a esa escena respetando patrón de ruta.    
    }

    /// <summary>
    /// Método para cargar las coordenadas de las habilidades muro/neblina asociados al mapa nombreMapa en el vector coordsMuros. EN CASO DE TENER DOS MAPAS HAY QUE CONFIGURARLO PARA QUE DISTINGA MUROS POR JUGADOR
    /// </summary>
    /// <param name="nombreMapa"></param>
    void LeeCordenadasPoderesDelMapa(string nombreMapa)
    {        StreamReader reader = new StreamReader(nombreMapa);                                                                       //Reader para leer el txt

       
            if (reader != null)
            {
                int cuantos = int.Parse(reader.ReadLine());                                                                           //La primera línea de cada txt con las coordenadas del mapa es la cantidad de coordenadas que hay
                coordsPoderesMapa = new Coordenadas[cuantos];                                                                         //Instanciamos el tamaño del vector de coordenadas con el número de coordenadas que hay

                for (int x = 0; x < coordsPoderesMapa.Length; x++)                                                                    //Recorremos tantas líneas a partir de ahí como coordenadas haya
                {
                    string linea = reader.ReadLine();                                                                                //En cada línea recogemos el texto. Esta trae coord x e y, separadas por un espacio
                    string[] coords = linea.Split(' ');                                                                              //Lo separamos por patrón de espacio
                    coordsPoderesMapa[x].x = float.Parse(coords[0]);                                                                 //Y asignamos cada valor a la correspondiente coordenada
                    coordsPoderesMapa[x].y = float.Parse(coords[1]);
                }
            }
         
        reader.Close();
    }

    /// <summary>
    /// Método para activar el muro, checkea la posición del jugador contrario e instancia el muro en la siguiente posición permitida cuando el poder sea activado                       
    /// </summary>
    void ActivaMuro()
    {
        int bandera = 0;                                                                                                          //Bandera para recorrer el array de coordenadas de poderes en mapa
        CalculaPosicionPoder(ref bandera);
        Instantiate(muro, new Vector3(coordsPoderesMapa[bandera].x, coordsPoderesMapa[bandera].y, 0), Quaternion.identity);       //Instanciamos el muro en la siguiente coordenada al jugador contrario
    }

    /// <summary>
    /// Método para activar el muro, checkea la posición del jugador contrario e instancia la neblina en la siguiente posición permitida cuando el poder sea activado   
    /// </summary>
    void ActivaNeblina()
    {
        //MÉTODO 1
        /*
        int bandera = 0;                                                                                                          //Bandera para recorrer el array de coordenadas de poderes en mapa
        CalculaPosicionPoder(ref bandera);
        float ancho = coordsPoderesMapa[bandera + 1].x - coordsPoderesMapa[bandera].x;                                            //Calculamos el ancho que tendrá la Neblina, dependerá de la distancia entre dos coordenadas sucesivas
        var niebla = Instantiate(neblina, new Vector3(coordsPoderesMapa[bandera].x, coordsPoderesMapa[bandera].y, 0), Quaternion.identity) as GameObject;      //Instanciamos la neblina en la siguiente coordenada al jugador contrario
        niebla.transform.localScale = new Vector3(ancho, 40f, 0f);                                                                  //Configuramos su tamaño
        niebla.layer = LayerMask.NameToLayer("Neblina");                                                                          //Acomodamos la Neblina en su layer
        */

        //MÉTODO 2 -> Se cambia temporalmente hasta tener los mapas definitivos en los que poder asignar coordenadas "reales" en los txt
        if (jugador == Player.jugador1)
            neb2.active = true;
        else
            neb1.active = true;

        Invoke("DesactivaNeblina", tiempoNeblina);
    }

    void DesactivaNeblina()
    {
        if (jugador == Player.jugador1)
            neb2.active = false;
        else
            neb1.active = false;
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
            transformJC = jugadorContrario.GetComponent<Transform>();                                                            //Si el JC está activo guarda su transform para gestionar conocer su posición

            while (transformJC.position.x >= coordsPoderesMapa[bandera].x && bandera + 1 < coordsPoderesMapa.Length)                 //Mientras que la coordenada del jugador contrario sea menor que la del muro en esa posición sigue avanzando
            {
                bandera++;
            }
        }
    }
}
