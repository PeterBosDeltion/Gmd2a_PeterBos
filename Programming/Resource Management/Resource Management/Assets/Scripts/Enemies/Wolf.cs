using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Wolf : Enemy {
    private bool hasWanderTarget;
	// Use this for initialization
	void Start () {
        GetMyComponents();
        SetState(States.Wander);
	}
	
	// Update is called once per frame
	void Update () {
        Die();
		if(state == States.Wander)
        {
            if(!hasWanderTarget) //Als ik niet aan het wanderen ben ga dat doen
            {
                Movement();
            }
            else if(Vector3.Distance(transform.position, agent.destination) <= .9F && hasWanderTarget) //Als het object bij de wander destination is calculate een nieuwe target
            {
                hasWanderTarget = false;
            }
        }
        else if(state == States.Fight) //Als het object moet vechten
        {
            if(target != null && !cooldown && Vector3.Distance(transform.position, target.transform.position) <= attackRange) //Als het een target heeft die inrange is
            {
                Fight();
                StartCoroutine(Cooldown());
            }
            else if(target == null) //Als er geen target is ga wanderen
            {
                SetState(States.Wander);
            }
        }
	}

    IEnumerator Cooldown() //Zorgt ervoor dat zijn enemy niet meteen dood is
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }

    public override void Movement() //Wander
    {
        //throw new NotImplementedException();
        //Set de destination in een range om dit object heen(kan netter)
        agent.SetDestination(new Vector3(transform.localPosition.x + Random.Range(-wanderDistance, wanderDistance), transform.localPosition.y, transform.localPosition.z + Random.Range(-wanderDistance, wanderDistance)));
        hasWanderTarget = true;
    }

    public override void Fight()
    {
        float crit = Random.value; //Berekent of er een crit is
        if (crit < critChance) //Zo ja doe damage * 3
        {
            int f = Random.Range(minDamage, maxDamage) * critMultiplier;
            target.GetComponent<Npc>().hp -= f;
        }
        else //Anders doe gewoon damage
        {
            int f = Random.Range(minDamage, maxDamage);
            target.GetComponent<Npc>().hp -= f;
        }
    }
}
