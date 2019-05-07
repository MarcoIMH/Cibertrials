using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour {
    bool estadoCheats;

    public Text cheatText;

    //Asegurarse de que solo hay una instancia
    public static Cheats instance = null;
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
        
        if (AnalizaCodigoIntroducido(codigo)) Debug.Log("Congratz! YOU ARE THE F. MASTER NOW! ENJOY THE POWER OF THE CIBERTRIAL'S GODS! "); //ActivarCheats();
        else cheatText.text = "Nothing to do..";
    }

    bool AnalizaCodigoIntroducido(string codigo)
    {
        if (codigo == "*-=G@DM0D3_0N:UCM_StYLe=-*") return true;
        return false;
    }

    void ActivarCheats()
    {
        estadoCheats = true;
    }

    public void DestroyCheats()
    {
        Destroy(this.gameObject);
    }

    public bool GetEstadoCheats()
    {
        return estadoCheats;
    }
}
