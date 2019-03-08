using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PoderesManager : MonoBehaviour {
    
    public GameObject jugadorContrario, muro, neblina;
    public Player jugador;
    public KeyCode teclaPoder;
    public int gemasMax;

    PerdidasControl per;

    
    struct Coordenadas
    {
        public float x;
        public float y;
    }
    Coordenadas[] coordsPoderesMapa;

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

        if(jugadorContrario.gameObject.GetComponent<PerdidasControl>() != null) per = jugadorContrario.gameObject.GetComponent<PerdidasControl>();

        Invoke("ConfiguraCoordenadasPoderes", 1f);
	}
	
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
                    per.ActivaInvierteControles();
                    break;
                case Poderes.cubito:
                    per.AplicarCuboDeHielo();
                    break;
                case Poderes.muro:
                    ActivaMuro();
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

    void BuscaHabilidad()
    {
        poderUsar = poder[Random.Range(3,4)];                  //Lo pongo en 3,4 para forzar que use neblina. Por defecto es 0 4. Devolver a la normalidad cuando termine de hacer los poderes
    }
    
    /// <summary>
    /// Método para configurar el poder del muro, solicita al GameManager el nombre de la escena y llama al método para cargar coordenadas de ese mapa
    /// </summary>
    void ConfiguraCoordenadasPoderes()
    {
        string mapa = GameManager.instance.NombreEscena();                                                           //Preguntamos a GameManager por el nombre de la escena
        LeeCordenadasPoderesDelMapa("Assets/Scripts/Mapa/MurosMapa/"+mapa+".txt");                                        //Cargamos el archivo correspondiente a esa escena        
    }

    /// <summary>
    /// Método para cargar las coordenadas de las habilidades muro/neblina asociados al mapa nombreMapa en el vector coordsMuros. EN CASO DE TENER DOS MAPAS HAY QUE CONFIGURARLO PARA QUE DISTINGA MUROS POR JUGADOR
    /// </summary>
    /// <param name="nombreMapa"></param>
    void LeeCordenadasPoderesDelMapa(string nombreMapa)
    {
        StreamReader reader = new StreamReader(nombreMapa);

        if (reader != null)
        {
            int cuantos = int.Parse(reader.ReadLine());                                                                   //La primera línea de cada txt con las coordenadas del mapa es la cantidad de coordenadas que hay
            coordsPoderesMapa = new Coordenadas[cuantos];                                                                       //Instanciamos el vector de coordenadas con el número de coordenadas que hay

            for (int x = 0; x < coordsPoderesMapa.Length; x++)                                                                  //Recorremos tantas líneas a partir de ahí como coordenadas haya
            {
                string linea = reader.ReadLine();                                                                         //En cada línea recogemos el texto
                string[] coords = linea.Split(' ');                                                                       //Lo separamos
                coordsPoderesMapa[x].x = float.Parse(coords[0]);                                                                //Y asignamos cada valor a la correspondiente coordenada
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
        int bandera = 0;                                                                                             //Bandera para recorrer el array de coordenadas de poderes en mapa
        CalculaPosicionPoder(ref bandera);
        Instantiate(muro, new Vector3(coordsPoderesMapa[bandera].x, coordsPoderesMapa[bandera].y, 0), Quaternion.identity);      //Instanciamos el muro en la siguiente coordenada al jugador contrario
    }

    /// <summary>
    /// Método para activar el muro, checkea la posición del jugador contrario e instancia la neblina en la siguiente posición permitida cuando el poder sea activado   
    /// </summary>
    void ActivaNeblina()
    {
        int bandera = 0;                                                                                              //Bandera para recorrer el array de coordenadas de poderes en mapa
        CalculaPosicionPoder(ref bandera);
        float ancho = coordsPoderesMapa[bandera + 1].x - coordsPoderesMapa[bandera].x;
        var niebla = Instantiate(neblina, new Vector3(coordsPoderesMapa[bandera].x, coordsPoderesMapa[bandera].y, 0), Quaternion.identity) as GameObject;      //Instanciamos la neblina en la siguiente coordenada al jugador contrario
        niebla.transform.localScale = new Vector3(ancho,40f,0f);
        niebla.layer = LayerMask.NameToLayer("Neblina");
    }

    /// <summary>
    /// Método para calcular la posición en la que se lanzará el poder Muro o Neblina
    /// </summary>
    /// <param name="bandera"></param>
    void CalculaPosicionPoder(ref int bandera)
    {
        Transform transformJC;
        if (jugadorContrario.GetComponent<Transform>() != null)
        {
            transformJC = jugadorContrario.GetComponent<Transform>();

            while (transformJC.position.x >= coordsPoderesMapa[bandera].x && bandera+1<coordsPoderesMapa.Length)                 //Mientras que la coordenada del jugador contrario sea menor que la del muro en esa posición sigue avanzando
            {
                bandera++;
            }
        }
    }
}
