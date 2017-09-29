using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    public bool drag;
    public Vector3 screenPoint;
    public Vector3 offset;
	// Use this for initialization
	void Start () {
        drag = false;
	}
	
	// Update is called once per frame
	void Update () {
      
       

    }

    public void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    public void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = Vector3.MoveTowards(transform.position,cursorPosition, 0.090F);
    }
  
}
