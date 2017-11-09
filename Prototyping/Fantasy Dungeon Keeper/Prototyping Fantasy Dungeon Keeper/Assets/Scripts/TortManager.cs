using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TortManager : MonoBehaviour {
    public static bool isTorturing;
    public static Vector3 tortPos;

    public static GameObject beingTorted;

    public static int infoGathered;
    public Text infoGatherText;

    public static int goalInfo;
	// Use this for initialization
	void Start () {
        tortPos = GameObject.FindGameObjectWithTag("TortPos").transform.position;
        goalInfo = 1000;
	}
	
	// Update is called once per frame
	void Update () {
        infoGatherText.text = "Info gathered: " + infoGathered;
	}

    public static void Leave()
    {
        Enemy curen = beingTorted.GetComponent<Enemy>();
        beingTorted.transform.position = curen.startPos;
        curen.StartCoroutine(curen.Recover());
        isTorturing = false;
        beingTorted = null;
    }

    
}
