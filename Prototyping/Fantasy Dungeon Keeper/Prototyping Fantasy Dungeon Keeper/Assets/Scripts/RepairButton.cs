using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairButton : MonoBehaviour {
    public Jailbar jb;
    public GameObject jbo;

    public Gamemanager gm;
	// Use this for initialization
	void Start () {
        jb = GetComponentInChildren<Jailbar>();
        jbo = GetComponentInChildren<Jailbar>().gameObject;

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
        }
        else if (!jbo.activeSelf)
        {
            jbo.SetActive(true);
            jb.hP = 1;
            Cursor.SetCursor(gm.pointCurs, Vector2.zero, CursorMode.Auto);

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
}
