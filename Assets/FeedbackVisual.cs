using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackVisual : MonoBehaviour {

    public GameObject stun, ralentizar, powerupPico, powerupFantasma, resucitar, powerUpVel, inversion, despacio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivarDesactivarFeedBack(int caso, bool actdes)
    {
        switch (caso)
        {
            case 0://stun
                stun.SetActive(actdes);
                break;
            case 1://ralentizar
                ralentizar.SetActive(actdes);
                break;
            case 2://ppPico
                powerupPico.SetActive(actdes);
                break;
            case 3://ppFan
                powerupFantasma.SetActive(actdes);
                break;
            case 4://resucitar
                resucitar.SetActive(actdes);
                break;
            case 5://ppVel
                powerUpVel.SetActive(actdes);
                break;
            case 6://inversion de controles
                inversion.SetActive(actdes);
                break;
            case 7://inversion de controles
                despacio.SetActive(actdes);
                break;
        }
    }

}
