using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public Vector3 target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 20 * Time.deltaTime);
            if(transform.position.x != target.x && transform.position.z != target.z)
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, 0);
            }
        }
        
	}


}
