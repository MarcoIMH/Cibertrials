using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesManager : MonoBehaviour {

    public Player tipoJugador;
    public KeyCode teclaSaltar, teclaRodar, teclaPoder, teclaRomperParedes, teclaSaltoParedes, teclaIzqdaParedes, teclaDchaParedes, teclaMenu;

    Dictionary<string, KeyCode> controles = new Dictionary<string, KeyCode>();

    // Use this for initialization
    void Start () {
        CreaDiccionarioControles();
        AsignaControlesJugador();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreaDiccionarioControles()
    {
        controles.Add("Saltar", teclaSaltar);
        controles.Add("Rodar", teclaRodar);
        controles.Add("Poder", teclaPoder);
        controles.Add("RomperParedes", teclaRomperParedes);
        controles.Add("SaltoParedes", teclaSaltoParedes);
        controles.Add("IzqdaParedes", teclaIzqdaParedes);
        controles.Add("DchaParedes", teclaDchaParedes);
        controles.Add("Menu", teclaMenu);
    }

    void AsignaControlesJugador()
    {
        SetControlSaltar(teclaSaltar);
        SetControlRodar(teclaRodar);

        SetControlPoder(teclaPoder);

        SetControlRomperParedes(teclaRomperParedes);

        SetControlSaltoParedes(teclaSaltoParedes);
        SetControlIzqdaParedes(teclaIzqdaParedes);
        SetControlDchaParedes(teclaDchaParedes);

        SetControlMenu(teclaMenu);        

        GameManager.instance.SetControlesJugador(controles, tipoJugador);
    }

    void SetControlSaltar(KeyCode nuevaTecla)
    {
        if (GetComponent<ControladorJugador>() != null)
            GetComponent<ControladorJugador>().SetTeclaSaltar(nuevaTecla);
    }

    void SetControlRodar(KeyCode nuevaTecla)
    {
        if (GetComponent<ControladorJugador>() != null)
            GetComponent<ControladorJugador>().SetTeclaRodar(nuevaTecla);
    }

    void SetControlPoder(KeyCode nuevaTecla)
    {
        if (GetComponent<PoderesManager>() != null)
            GetComponent<PoderesManager>().SetTeclaPoder(nuevaTecla);
    }

    void SetControlRomperParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<RomperParedes>() != null)
            GetComponent<RomperParedes>().SetTeclaRomperParedes(nuevaTecla);
    }

    void SetControlSaltoParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaSaltoParedes(nuevaTecla);
    }

    void SetControlIzqdaParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaIzqdaParedes(nuevaTecla);
    }

    void SetControlDchaParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaDchaParedes(nuevaTecla);
    }

    void SetControlMenu(KeyCode nuevaTecla)
    {
        GameManager.instance.SetTeclaMenu(nuevaTecla, tipoJugador);
    }

    public void CambiaControlJugador(string nombreControl, KeyCode nuevaTecla)
    {
        switch (nombreControl)
        {
            case "Saltar": SetControlSaltar(nuevaTecla); break;
            case "Rodar": SetControlRodar(nuevaTecla); break;
            case "Poder": SetControlPoder(nuevaTecla); break;
            case "RomperParedes": SetControlRomperParedes(nuevaTecla); break;
            case "SaltoParedes": SetControlSaltoParedes(nuevaTecla); break;
            case "IzqdaParedes": SetControlIzqdaParedes(nuevaTecla); break;
            case "DchaParedes": SetControlDchaParedes(nuevaTecla); break;
            case "Menu": SetControlMenu(nuevaTecla); break;
        }
    }
}
