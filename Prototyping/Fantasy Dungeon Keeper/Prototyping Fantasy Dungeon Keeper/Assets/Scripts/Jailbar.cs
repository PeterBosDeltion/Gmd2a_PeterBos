using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jailbar : MonoBehaviour {
    public int hP;
    public int startHp;
    private int halfHp;
    public ParticleSystem part;
    private bool isPlaying;
	// Use this for initialization
	void Start () {
        startHp = hP;
        halfHp = hP/2;
        part = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if(hP <= halfHp)
        {

            if (!isPlaying)
            {
                part.Play();
                isPlaying = true;
            }
        }

        if(hP > halfHp)
        {
            part.Stop();
        }
		if(hP <= 0)
        {
            gameObject.SetActive(false);
            isPlaying = false;
        }
	}
}
