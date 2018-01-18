using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Npc : MonoBehaviour {
    [Header("Components")]
    public CraftingManager cm;
    [Header("NavMesh")]
    public GameObject target;
    public NavMeshAgent agent;
    [Header("Job variables")]
    public int hp;
    public float idleTime;
    public float doJobCooldown;
    public List<string> inventory = new List<string>();
    public int maxInvSize;

    public GameObject myTool;

    public bool idling;

    public enum States
    {
        Gather,
        BringResource,
        Idle,
        Create,
        Find,
        Fight
    }

    public States state;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetTarget(string tagToFind) //Deze funcite vindt het dichtsbijzeinde object met de tag die je hem geeft
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag(tagToFind);
        float dist = Mathf.Infinity;
        
        foreach (GameObject g in allTargets)
        {
            Vector3 diff = g.transform.position - transform.position;
            float curDust = diff.sqrMagnitude;
            if (curDust < dist)
            {
                target = g;
                agent.destination = target.transform.position;
                dist = curDust;
            }
        }
    }

    public void SetState(States s) //Deze functie zet de state van iets
    {
        state = s;
    }


    public void GetMyComponents() //Dit is eigenlijk gewoon een start functie voor alles wat van dit script inherit
    {
        cm = GameObject.FindGameObjectWithTag("GM").GetComponent<CraftingManager>();
        agent = GetComponent<NavMeshAgent>();

    }

    public abstract void DoJob(); //Deze functie doet in elk script wat hiervan inherit iets anders


    public void Die()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
