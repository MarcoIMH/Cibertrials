using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCoordenadasMapa : MonoBehaviour {

    Transform[] spawns;

	// Use this for initialization
	void Start () {
        spawns = GetComponentsInChildren<Transform>();
        OrdenaCoordenadasSpawn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OrdenaCoordenadasSpawn()
    {
        for(int x = 0; x+1 < spawns.Length; x++)
        {
            for(int j=x+1; j<spawns.Length;j++)
                if(spawns[x].position.x > spawns[j].position.x)
                {
                    Transform trAux = spawns[x];
                    spawns[x] = spawns[j];
                    spawns[j] = trAux;
                }
        }
        for (int x = 0; x < spawns.Length; x++)
            Debug.Log(spawns[x].position.x + "," + spawns[x].position.y);
        GameManager.instance.SetCoordenadasPoderes(spawns);
        Destroy(this);
    }
}
