using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oddnums : MonoBehaviour {
    public List<int> randomInts = new List<int>();
    // Use this for initialization
    void Start () {
        SortList(randomInts);

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

    public List<int> SortList(List<int> myList)
    {
        List<int> newList = new List<int>();
        int y = new int();
        
        while (y < Mathf.Max(myList.ToArray()))
        {
            y = Mathf.Min(myList.ToArray());
        
                newList.Add(Mathf.Min(myList.ToArray()));
                myList.Remove(y);
     
           
        }
        
        foreach(int u in newList)
        {
            Debug.Log(u);
        }
        return newList;
    }
}
