using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject targetGate;
    public GameObject exit;


    public float speed;
    public bool canAttack;

	// Use this for initialization
	void Start () {
        canAttack = true;
        GameObject[] gates;
        gates = GameObject.FindGameObjectsWithTag("Gate");
        float dist = Mathf.Infinity;
        Vector3 pos = transform.position;
        foreach(GameObject g in gates)
        {
            Vector3 diff = g.transform.position - transform.position;
            float curDust = diff.sqrMagnitude;
            if(curDust < dist)
            {
                targetGate = g;
                dist = curDust;
            }
        }

        exit = GameObject.FindGameObjectWithTag("Exit");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        AttackGate();
        Move();
    }

    public void Move()
    {
        if (!targetGate.activeSelf)
        {
            Vector3 vec = new Vector3(exit.transform.position.x, transform.position.y, exit.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, vec, speed * Time.deltaTime);
            
            
        }
    }

    public void AttackGate()
    {
        if (targetGate.activeSelf)
        {
           transform.position = Vector3.MoveTowards(transform.position, targetGate.transform.position, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, targetGate.transform.position) <= 1)
            {
                if (canAttack)
                {
                    targetGate.GetComponent<Jailbar>().hP -= 1;
                    canAttack = false;
                    StartCoroutine(Att());
                }
            }
        }
    }

    IEnumerator Att()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
}
