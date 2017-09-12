using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oddnums : MonoBehaviour {
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<int> GetOddNumbers(List<int> l)
    {

        List<int> oddNumbers = new List<int>();
       foreach(int i in l)
        {
            if(i%2 == 0)
            {
                //Even
            }
            else
            {
                oddNumbers.Add(i);
                foreach(int y in oddNumbers)
                {
                    Debug.Log(y);
                }
            }
        }
        return oddNumbers;
    }
}
