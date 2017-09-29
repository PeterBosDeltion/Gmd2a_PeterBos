using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiskunde : MonoBehaviour {
    public float honderdDag;
    public float centDag;
	// Use this for initialization
	void Start () {
        honderdDag = 100 * 30;
        centDag = 0.01F * Mathf.Pow(2, 30);

        Debug.Log(honderdDag + "Honderd per dag");
        Debug.Log(centDag + "Cents");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
