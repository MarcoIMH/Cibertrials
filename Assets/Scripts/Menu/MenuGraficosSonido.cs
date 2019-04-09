﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGraficosSonido : MonoBehaviour {

    public Dropdown resolucionesDropDown, calidadGraficaDropDown;

    public AudioSource musica;

    Resolution[] resoluciones;

    bool pantallaCompleta;
    int indiceGraficos, indiceResolucion;
    Resolution resolucionActual;

    float volumenSonidos;

    // Use this for initialization
    void Start () {
        resoluciones = Screen.resolutions;
        ConfiguraDropDownResoluciones();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Método para automatizar el paso de las distintas resoluciones existentes al dropdown de resoluciones.
    /// </summary>
    void ConfiguraDropDownResoluciones()
    {
        if (resoluciones != null && resolucionesDropDown!=null)
        {
            resolucionesDropDown.ClearOptions();
            List<string> opciones = new List<string>();

            int resolucionActual = 0;
            for (int x = 0; x < resoluciones.Length; x++)
            {
                string opcion = resoluciones[x].width + " x " + resoluciones[x].height;
                opciones.Add(opcion);

                if (resoluciones[x].width == Screen.currentResolution.width &&
                    resoluciones[x].height == Screen.currentResolution.height)
                    indiceResolucion = resolucionActual = x;
            }

            resolucionesDropDown.AddOptions(opciones);
            resolucionesDropDown.value = resolucionActual;
            resolucionesDropDown.RefreshShownValue();
        }        
    }

    void ConfiguraDropDownCalidadGraficos()
    {
        if (calidadGraficaDropDown != null)
        {
            calidadGraficaDropDown.ClearOptions();

            List<string> calidades = new List<string>();
            calidades.Add("Bajos");
            calidades.Add("Medios");
            calidades.Add("Altos");

            calidadGraficaDropDown.AddOptions(calidades);
            calidadGraficaDropDown.value = indiceGraficos;        
            calidadGraficaDropDown.RefreshShownValue();
        }
    }

    /// <summary>
    /// Método para controlar el juego en pantalla completa o no. Trabaja con el Toogle de MenuConfiguracionIngame (interfaz Unity). 
    /// </summary>
    /// <param name="pantallaCompleta"></param>
    public void SetPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        this.pantallaCompleta = pantallaCompleta;
    }

    /// <summary>
    /// Método para controlar los gráficos del juego. Trabaja con el DropDown de MenuConfiguracionIngame (interfaz Unity). 
    /// </summary>
    /// <param name="indiceGraficos"></param>
    public void SetNivelGraficos(int indiceGraficos)
    {
        QualitySettings.SetQualityLevel(indiceGraficos);
        this.indiceGraficos = indiceGraficos;
    }

    /// <summary>
    /// Configura el tamaño de la resolución de pantalla. Trabaja con el DropDown de MenuConfiguracionIngame (interfaz Unity). 
    /// </summary>
    public void SetResolucion(int indiceResolucion)
    {
        Screen.SetResolution(resoluciones[indiceResolucion].width, resoluciones[indiceResolucion].height, Screen.fullScreen);
        resolucionActual.width = resoluciones[indiceResolucion].width;
        resolucionActual.height = resoluciones[indiceResolucion].height;
    }

    /// <summary>
    /// Método para gestionar el volúmen de los sonidos. Trabaja con el slider de MenuConfiguracionIngame (interfaz UnitY). 
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumenSonidos(float vol)
    {
        volumenSonidos = vol;
    }

    /// <summary>
    /// Método para gestionar el volúmen de los sonidos. Trabaja con el slider de MenuConfiguracionIngame (interfaz UnitY). 
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolumenMusica(float vol)
    {
        musica.volume = vol;
    }

    /// <summary>
    /// Tras cerrar el menú de gráficos almacenamos en GameManager la nueva configuración de gráficos y sonido para no perderla tras un posible cambio de escena.
    /// </summary>
    public void GuardaConfiguracionGraficosSonidos()
    {
        if (!Controles.instance.GetEnMenuPrincipal())
        {
            GameManager.instance.GuardaConfiguracionGraficos(pantallaCompleta, indiceGraficos, indiceResolucion, resolucionActual);
            GameManager.instance.SetVolumenSonidos(volumenSonidos);
            //GameManager.instance.SetVolumenMusica(musica.volume);
        }        
    }
}
