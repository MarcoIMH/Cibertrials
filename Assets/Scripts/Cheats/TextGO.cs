using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGO : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (CheatsManager.instance != null) CheatsManager.instance.SetGameObjectText(this.gameObject.GetComponent<Text>());
        Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
