using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonGO : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecogerTextoInputField()
    {
        if (CheatsManager.instance != null) CheatsManager.instance.RecogeTextoInputFieldCheats();
    }
}
