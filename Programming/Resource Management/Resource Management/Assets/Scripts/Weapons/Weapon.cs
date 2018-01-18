using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [Header("Combat Stats")]
    public int minDamage;
    public int maxDamage;
    public float critChance;
    public int critMultiplier;
    public float attackRange;

    [Header("Crafting Needs")]
    public int amounOfBarsNeeded;
    public string oreNeeded;
    public string barNeeded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
