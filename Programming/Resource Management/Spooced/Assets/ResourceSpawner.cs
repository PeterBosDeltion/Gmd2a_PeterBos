using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour {
    public List<GameObject> resourcePrefabs = new List<GameObject>();
    public GameObject plane;
    public GameObject ship;
    public int maxResources = 5;
    public int currentResources;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(currentResources < maxResources)
        {
            Generate();
            currentResources++;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 v = new Vector3(hit.point.x, 20, hit.point.z);

                GameObject g = Instantiate(ship, new Vector3(30, 100, 30), Quaternion.identity);
                g.GetComponent<Ship>().target = v;


                // Do something with the object that was hit by the raycast.
            }
        }
    }

    void Generate()
    {
  
        float xf = Random.Range(-95, 95);
        float zf = Random.Range(-95, 95);

        Vector3 v = new Vector3(xf, 1, zf);
        Instantiate(resourcePrefabs[0], v, Quaternion.identity);
    }
}
