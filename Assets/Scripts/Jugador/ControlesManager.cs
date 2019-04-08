using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesManager : MonoBehaviour {

    public Player tipoJugador;

    Dictionary<string, KeyCode> controles = new Dictionary<string, KeyCode>();
    bool cargaInicial = true;

    // Use this for initialization
    void Start () {
        Invoke("AsignaControlesInicialesJugador", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AsignaControlesInicialesJugador()
    {
        controles = Controles.instance.GetControlesJugador(tipoJugador);

        SetControlSaltar(controles["Saltar"]);
        SetControlRodar(controles["Rodar"]);

        SetControlPoder(controles["Poder"]);

        SetControlRomperParedes(controles["RomperParedes"]);

        SetControlIzqdaParedes(controles["IzqdaParedes"]);
        SetControlDchaParedes(controles["DchaParedes"]);

        SetControlMenu(controles["Menu"]);

        cargaInicial = false;
    }

    void SetControlSaltar(KeyCode nuevaTecla)
    {
        if (GetComponent<ControladorJugador>() != null)
            GetComponent<ControladorJugador>().SetTeclaSaltar(nuevaTecla);

        if(!cargaInicial) controles["Saltar"] = nuevaTecla;

        SetControlSaltoParedes(nuevaTecla);
    }

    void SetControlRodar(KeyCode nuevaTecla)
    {
        if (GetComponent<ControladorJugador>() != null)
            GetComponent<ControladorJugador>().SetTeclaRodar(nuevaTecla);

        if (!cargaInicial) controles["Rodar"] = nuevaTecla;
    }

    void SetControlPoder(KeyCode nuevaTecla)
    {
        if (GetComponent<PoderesManager>() != null)
            GetComponent<PoderesManager>().SetTeclaPoder(nuevaTecla);

        if (!cargaInicial) controles["Poder"] = nuevaTecla;
    }

    void SetControlRomperParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<RomperParedes>() != null)
            GetComponent<RomperParedes>().SetTeclaRomperParedes(nuevaTecla);

        if (!cargaInicial) controles["RomperParedes"] = nuevaTecla;
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

        if (!cargaInicial) controles["IzqdaParedes"] = nuevaTecla;
    }

    void SetControlDchaParedes(KeyCode nuevaTecla)
    {
        if (GetComponent<SaltoParedes>() != null)
            GetComponent<SaltoParedes>().SetTeclaDchaParedes(nuevaTecla);

        if (!cargaInicial) controles["DchaParedes"] = nuevaTecla;
    }

    void SetControlMenu(KeyCode nuevaTecla)
    {
        GameManager.instance.SetTeclaMenu(nuevaTecla, tipoJugador);

        if (!cargaInicial) controles["Menu"] = nuevaTecla;
    }

    void InformaControles()
    {
        Controles.instance.SetControlesJugador(controles, tipoJugador);
    }

    public void CambiaControlJugador(string nombreControl, KeyCode nuevaTecla)
    {
        switch (nombreControl)
        {
            case "Saltar": SetControlSaltar(nuevaTecla); break;
            case "Rodar": SetControlRodar(nuevaTecla); break;
            case "Poder": SetControlPoder(nuevaTecla); break;
            case "RomperParedes": SetControlRomperParedes(nuevaTecla); break;
            case "IzqdaParedes": SetControlIzqdaParedes(nuevaTecla); break;
            case "DchaParedes": SetControlDchaParedes(nuevaTecla); break;
            case "Menu": SetControlMenu(nuevaTecla); break;
        }
        InformaControles();
    }
}
