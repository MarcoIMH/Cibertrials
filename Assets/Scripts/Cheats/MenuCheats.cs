﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCheats : MonoBehaviour {

    public Dropdown dropDownCheats;

	// Use this for initialization
	void Start () {
        ConfiguraDropDownCheats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ConfiguraDropDownCheats()
    {
        if (dropDownCheats != null)
        {
            dropDownCheats.ClearOptions();
            List<string> opciones = new List<string>();
            for (int x = 1; x <= 3; x++) opciones.Add("Mapa " + x);

            dropDownCheats.AddOptions(opciones);

            dropDownCheats.value = 0;
            dropDownCheats.RefreshShownValue();
        }        
    }

    public void CargarMapa()
    {

    }
}
