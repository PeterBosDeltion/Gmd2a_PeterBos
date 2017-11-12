using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairButton : MonoBehaviour {
    public Jailbar jb;
    public GameObject jbo;

    public Gamemanager gm;

    public AudioSource audio;
    public AudioClip strike;
	// Use this for initialization
	void Start () {
        jb = GetComponentInChildren<Jailbar>();
        jbo = GetComponentInChildren<Jailbar>().gameObject;
        audio = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (jbo.activeSelf && jb.hP < jb.startHp)
        {
            jb.hP += 1;
            Cursor.SetCursor(gm.pointCurs, Vector2.zero, CursorMode.Auto);
            audio.PlayOneShot(strike, 1);
        }
        else if (!jbo.activeSelf)
        {
            jbo.SetActive(true);
            jb.hP = 1;
            Cursor.SetCursor(gm.pointCurs, Vector2.zero, CursorMode.Auto);
            audio.PlayOneShot(strike, 1);

        }
    }

    public void OnMouseUp()
    {
        Cursor.SetCursor(gm.defCurs, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(gm.defCurs, Vector2.zero, CursorMode.Auto);
    }

    public void Repair()
    {
        if (jbo.activeSelf && jb.hP < jb.startHp)
        {
            jb.hP += 1;
            Cursor.SetCursor(gm.pointCurs, Vector2.zero, CursorMode.Auto);
            audio.PlayOneShot(strike, 1);
        }
        else if (!jbo.activeSelf)
        {
            jbo.SetActive(true);
            jb.hP = 1;
            Cursor.SetCursor(gm.pointCurs, Vector2.zero, CursorMode.Auto);
            audio.PlayOneShot(strike, 1);

        }
    }

    public void WaveSound()
    {
        audio.PlayOneShot(strike, 1);
    }

    public void InvokeSound()
    {
        InvokeRepeating("WaveSound", 0.4F, 0.4F);
        StartCoroutine(Offsound());
    }

    public IEnumerator Offsound()
    {
        yield return new WaitForSeconds(2);
        CancelInvoke();
    }
}
