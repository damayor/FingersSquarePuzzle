using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugProves : MonoBehaviour {

    public GameObject izq;

    public GameObject der;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 posIzq = izq.transform.position;

            izq.transform.position = der.transform.position;
            der.transform.position = posIzq;
            
              
        }

	}
}
