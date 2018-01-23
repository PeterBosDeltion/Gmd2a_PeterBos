using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    public float scrollX;
    public float scrollY;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * 15 * Time.deltaTime);
    }
}
