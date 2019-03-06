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

    PerdidasControl per;
    
	void Start () {
        gemas = 0;
        habilidadActiva = false;

        //asignamos los poderes
        poder[0] = Poderes.cubito;
        poder[1] = Poderes.inversionControles;
        poder[2] = Poderes.muro;
        poder[3] = Poderes.neblina;

        if(jugadorContrario.gameObject.GetComponent<PerdidasControl>() != null) per = jugadorContrario.gameObject.GetComponent<PerdidasControl>();
	}
	
	// Update is called once per frame
	void Update () {

        //print(habilidadActiva);
        if(Input.GetKeyDown(teclaPoder) && habilidadActiva) 
        {
            //print("patata");
            ResetStats();
            habilidadActiva = false;
            switch (poderUsar)
            {
                case Poderes.inversionControles:
                    per.ActivaInvierteControles();
                    break;
                case Poderes.cubito:
                    per.AplicarCuboDeHielo();
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
        print(gemas);
        if (gemas < gemasMax)
        {
            gemas++;
            print("gemas:"+gemas);
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
        poderUsar = poder[Random.Range(0,4)];
    }
   
}
