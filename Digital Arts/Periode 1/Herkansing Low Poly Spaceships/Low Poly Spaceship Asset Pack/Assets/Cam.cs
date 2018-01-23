using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    public Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f"))
        {
            anim.enabled = true;
        }
	}
}
