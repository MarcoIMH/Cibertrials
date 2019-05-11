using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsManager : MonoBehaviour {
    bool estadoCheats, estadoInvencibilidad=false;

    public Text cheatText;

    //Asegurarse de que solo hay una instancia
    public static CheatsManager instance = null;
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
    void Start () {
        estadoCheats = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecogeTextoInputFieldCheats()
    {
        
        string codigo = cheatText.text;
        Debug.Log("Codigo introducido: "+codigo);

        AnalizaCodigoIntroducido(codigo);
    }

    void AnalizaCodigoIntroducido(string codigo)
    {
        //if (codigo == "*-=G@DM0D3_0N:UCM_StYLe=-*")
        if (codigo == "12345")
        {
            Debug.Log("Congratz! YOU ARE THE F. MASTER NOW! ENJOY THE POWER OF THE CIBERTRIAL'S GODS! ");
            SetCheats(true);
        }
        else if (codigo == "*-=G@DM0D3_0FF:UCM_StYLe=-*")
        {
            Debug.Log("Loosing Cibertrial's Powers! MAY THE FORCE BE WITH YOU");
            SetCheats(false);
        }
        else
        {
            Debug.Log("Are you keeding me? Nothing to do... :D ");
            cheatText.text = "Nothing to do..";
        } 
    }

    void SetCheats(bool estado)
    {
        estadoCheats = estado;
    }

    public void DestroyCheats()
    {
        Destroy(this.gameObject);
    }

    public bool GetEstadoCheats()
    {
        return estadoCheats;
    }

    public void SetEstadoInvencibilidad(bool est)
    {
        estadoInvencibilidad = est;
    }

    public bool GetEstadoInvencibilidad()
    {
        return estadoInvencibilidad;
    }
}
