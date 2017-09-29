using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairButton : MonoBehaviour {
    public Jailbar jb;
    public GameObject jbo;
	// Use this for initialization
	void Start () {
        jb = GetComponentInChildren<Jailbar>();
        jbo = GetComponentInChildren<Jailbar>().gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (jbo.activeSelf && jb.hP < jb.startHp)
        {
            jb.hP += 1;
        }
        else if (!jbo.activeSelf)
        {
            jbo.SetActive(true);
            jb.hP = 1;
            
        }
    }
}
