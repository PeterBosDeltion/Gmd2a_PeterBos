using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    public bool drag;
    public Vector3 screenPoint;
    public Vector3 offset;

    public Gamemanager gm;
    public LineRenderer ln;
	// Use this for initialization
	void Start () {
        drag = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>();
        ln = gm.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
      
       

    }

    public void OnMouseDown()
    {
        ln.enabled = true;
        Cursor.SetCursor(gm.dragCurs, Vector2.zero, CursorMode.Auto);
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        ln.SetPosition(0, transform.position);
    }

    public void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = Vector3.MoveTowards(transform.position,cursorPosition, 0.090F);

        ln.SetPosition(1, cursorPosition);
    }

    public void OnMouseUp()
    {
        Cursor.SetCursor(gm.defCurs, Vector2.zero, CursorMode.Auto);

        ln.enabled = false;
    }
  
}
