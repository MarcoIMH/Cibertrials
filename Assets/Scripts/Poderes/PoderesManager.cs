using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoderesManager : MonoBehaviour {

    public int gemasMax;
    public GameObject jugadorContrario;
    public Player jugador;
    public KeyCode teclaPoder;

    int gemas;
    Poderes[] poder = new Poderes[4]; //array de poderes
    Poderes poderUsar;
    bool habilidadActiva;


    
	void Start () {
        gemas = 0;
        habilidadActiva = false;

        //asignamos los poderes
        poder[0] = Poderes.cubito;
        poder[1] = Poderes.cambioControles;
        poder[2] = Poderes.muro;
        poder[3] = Poderes.neblina;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKey(teclaPoder) && habilidadActiva) 
        {
            print("patata");
            ResetStats();
            habilidadActiva = false;
            switch (poderUsar)
            {
                case Poderes.cambioControles:
                    //codigo cambio controles
                    break;
                case Poderes.cubito:
                    //codigo cubito
                    break;
                case Poderes.muro:
                    //codigo muro 
                    break;
                case Poderes.neblina:
                    //codigo neblina
                    break;
            }

        }
	}

    public void ResetStats()
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
            return true;
        }
        
        else
        {
            print("comprueba");
            BuscaHabilidad();
            habilidadActiva = true;
            return false;
        }
       
            
    }
    void BuscaHabilidad()
    {
        //ramdom
        poderUsar = poder[Random.Range(0,4)];
    }
   
}
