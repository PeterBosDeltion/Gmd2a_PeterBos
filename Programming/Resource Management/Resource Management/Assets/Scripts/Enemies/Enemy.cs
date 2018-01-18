using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour {
    public NavMeshAgent agent;
    public GameObject target;

    public int health;
    public int minDamage;
    public int maxDamage;
    public float critChance;
    public int critMultiplier;
    public float attackRange;

    public float wanderDistance;

    public bool cooldown;
    public float cooldownTime;

    public enum States
    {
        Wander,
        Fight,
        Search
    }

    public States state;
	// Use this for initialization
	void Start () {
		
	}

   
    // Update is called once per frame
    void Update () {
	}

    public void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetState(States s) //Deze functie zet de state van iets
    {
        state = s;
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

    public void GetMyComponents()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public abstract void Movement();

    public abstract void Fight();
}
