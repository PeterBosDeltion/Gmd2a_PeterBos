using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enm : MonoBehaviour {
    public bool target;
    public Vector3 v;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Muuv();

    }

    public void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Move.score += 1;
        Move.currentE -= 1;
    }

    public void Muuv()
    {
        if (!target)
        {
             v = new Vector3(Random.Range(-12.9F, 11.41F), transform.position.y, Random.Range(-12.9F, 13.13F));
            target = true;
        }
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, v, 3 * Time.deltaTime);
            if(transform.position == v)
            {
                target = false;
            }
        }
    }
}
