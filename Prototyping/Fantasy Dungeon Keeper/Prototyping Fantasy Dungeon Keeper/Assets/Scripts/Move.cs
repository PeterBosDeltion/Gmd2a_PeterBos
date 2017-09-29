using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Move : MonoBehaviour {
    public float speed;
    public float scrollSpeed;
    public Vector3 mousePos;
    public float rotateSpeed;

    public float boundary;
    public float moveSensitiv;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Moove();
        Rotat();

        
    }

    public void Moove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float step = speed * Time.deltaTime;

        

        transform.Translate(Vector3.right * x * step);
        transform.Translate(Vector3.up * z * step);

        if(Input.mousePosition.x >= Screen.width - boundary )
        {
            transform.Translate(Vector3.right * moveSensitiv * Time.deltaTime);
        }
        else if (Input.mousePosition.x <= 0 + boundary)
        {
            transform.Translate(Vector3.right *- moveSensitiv * Time.deltaTime);
        }
        else if (Input.mousePosition.y >= Screen.height - boundary)
        {
            transform.Translate(Vector3.up * moveSensitiv * Time.deltaTime);
        }
        else if (Input.mousePosition.y <= 0 + boundary)
        {
            transform.Translate(Vector3.up *- moveSensitiv * Time.deltaTime);
        }
    }

    public void Rotat()
    {
        float f = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(Vector3.forward * f * scrollSpeed * Time.deltaTime);

        if (Input.GetMouseButton(2))
        {
             
            float x = Input.GetAxis("Mouse X");

            if(x >= 0)
            {
                mousePos = new Vector3(0, 0, x);

            }
            else if (x <= 0)
            {
                mousePos = new Vector3(0, 0, -x);

            }

            transform.Rotate(mousePos * x * rotateSpeed * Time.deltaTime);
        }
    }
}
