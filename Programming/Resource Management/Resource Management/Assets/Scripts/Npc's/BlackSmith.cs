using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : Npc {
    private int amountOfOreNeeded;
    private string oreNeeded;
    private GameObject thingToMake;

    private bool coolingDown;
    private bool smeltedOre;
    private bool hasOre;
    private bool hasBars;
    private GameObject carrying;
	// Use this for initialization
	void Start () {
        GetMyComponents();
        SetState(States.Idle);
        SetThingToMake(cm.allWeapons[0]); //Zet het ding om te maken naar een zwaard
     
    }

    // Update is called once per frame
    void Update () {
        if (!idling && state == States.Idle) //Als de state idle is ga idlen
        {
            StartCoroutine(Idle());
        }
        else if(state == States.Gather) //Als de state gather is ga de benodigde resources halen
        {
            SetTarget("SmithChest"); //Zoekt de dichtsbijzijnde chest
            if(Vector3.Distance(transform.position, target.transform.position) <= 2)
            {
                if(inventory.Count < maxInvSize) //Als het object binnen bereik is en er is ruimte in zijn inventory
                {
                    
                    GetStuffFromChest(); //Haalt de resources uit de kist
                    if(inventory.Count > 0) //Als er wat in zat ga een ding maken
                    {
                        SetState(States.Create);
                    }
                    else //Anders gaat het object weer idlen
                    {
                        SetState(States.Idle);
                    }
                }
            }
        }
        else if(state == States.Create) //Als het object iets moet maken gaat hij dat doen
        {
            DoJob();
        }
       
        else if(state == States.BringResource) //Als het object iets heeft gemaakt brengt hij het naar de opslag
        {
                SetTarget("Storage"); //Zet de target naar de dichtsbijzijnde opslag
               if(Vector3.Distance(transform.position, target.transform.position) <= 4) //Als het object dichtbij de opslag is
                {
                    Debug.Log("NEAR STORAGE");
                    Storage st = target.GetComponent<Storage>();
                    carrying.transform.SetParent(target.transform);
                    carrying.transform.localPosition = Vector3.zero;
                    st.contents.Add(carrying); //Geeft het gemaakte ding aan de opslag zodat iets anders het kan gaan gebruiken
                    carrying = null;
                    SetState(States.Idle); //Object gaat weer idlen
            }

        }
	}

    void SetThingToMake(GameObject makeThis) //Dit zet het ding dat gemaakt moet worden
    {
        thingToMake = makeThis;
        Weapon w = thingToMake.GetComponent<Weapon>();
        if(w != null) //Als w een wapen is
        {
            oreNeeded = thingToMake.GetComponent<Weapon>().oreNeeded; //Zet het type ore dat nodig is voor dit wapen
            amountOfOreNeeded = thingToMake.GetComponent<Weapon>().amounOfBarsNeeded; //Zet het aantal ore dat nodig is voor dit wapen
        }

    }

    IEnumerator Idle() //Object doet niets
    {
        idling = true;
        yield return new WaitForSeconds(idleTime);
        SetState(States.Gather); //Ga kijken of er alweer iets te doen is, anders gaat het object weer idlen
        idling = false;

    }

    void GetStuffFromChest() //Dit haalt de benodigde dingen uit een chest
    {
        Chest chest = target.GetComponent<Chest>();
        if(chest.contents.Count > 0) //Als de chest niet leeg is
        {
            List<string> tempList = new List<string>(); //Tijdelijke kopie van de list
            foreach (string q in chest.contents)
            {
                tempList.Add(q);
            }
            foreach (string g in chest.contents)
            {
                if (g == oreNeeded) //Als het de ore is die nodig is
                {

                    if (amountOfOreNeeded > 0) //En als het object nog niet genoeg heeft
                    {
                        inventory.Add(g); //Add de ore aan de inventory
                        tempList.Remove(g); //Haal de ore uit de tijdelijke list

                        amountOfOreNeeded--; //Aantal dat nog in de inventory moet wordt minder

                    }
                    else //Anders stoppen met de functie
                    {
                        //return;
                    }

                }


            }
            chest.contents.Clear(); //Leeg de chest
            foreach (string s in tempList) //Add dan alles wat niet naar dit object zijn inventory is gegaan terug naar de chest
            {
                chest.contents.Add(s); //Note to self: Vraag Alex waarom dit een error geeft terwijl het gewoon werkt zoals het hoort
            }
        }
        else //Als de chest leeg is ga idlen
        {
            SetState(States.Idle);
        }




    }

    public override void DoJob() //Maak de bars en het wapen waarom wordt gevraagd
    {
        if (!coolingDown) //Zorgt ervoor dat de functie niet duizenden keren wordt gecalled
        {
            coolingDown = true;
            int amountOfBars = 0; //Aantal bars in inventory
            foreach (string s in inventory) //Loopt door de inventory heen
            {
                if (s.Contains("Ore")) //Als er ore in de inventory is
                {
                    hasOre = true;
                }
                else if (s.Contains("Bar")) //Als er bars in de inventory zijn
                {
                    hasBars = true;
                    amountOfBars++;
                }
                
            }
            if (!smeltedOre && hasOre) //Als er ore in de inventory zit en het is nog niet gesmolten
            {
                SetThingToMake(cm.allBars[0]); //Zet het ding om te maken naar een iron bar
                SetTarget("SmithFurnace"); //Ga naar het furnace
                Smelt(); //Smelt de ore
            }
            else if(hasBars && amountOfBars >= thingToMake.GetComponent<Weapon>().amounOfBarsNeeded) //Als er bars in de inventory zitten en er zijn genoeg om het ding te maken
            {
                SetTarget("Anvil"); //Ga naar het anvil
                if (Vector3.Distance(transform.position, target.transform.position) <= 2) //Als het object dichtbij het anvil is
                {
                    MakeWeapon(); //Maakt het wapen
                    smeltedOre = false; //Reset de bool zodat er weer ore gesmolten kan worden
                }
            }
            else //Als er geen ore en/of bars in de inventory zitten ga idlen
            {
                SetState(States.Idle);
            }
            StartCoroutine(CoolDown()); //Reset de coolingDown bool
        }
        //throw new NotImplementedException();
    }

    void MakeWeapon() //Maakt het wapen
    {
        GameObject anvil = target;
        int amountInInv = new int(); //Aantal van de bars die nodig zijn
        foreach (string s in inventory)
        {
           if(s == thingToMake.GetComponent<Weapon>().barNeeded) //Als de benodigde bar in de inventory zit tel de int op
            {
                amountInInv++;
            }
        }
            if (inventory.Contains(thingToMake.GetComponent<Weapon>().barNeeded) && amountInInv == thingToMake.GetComponent<Weapon>().amounOfBarsNeeded) //Als de benodigde bar in de inventory zit en er zijn er genoeg
        {

                foreach (Transform t in anvil.transform) //Loop door de anvil zijn children heen
                {
                    if (t.transform.gameObject.name == "AnvilPrefabSpawner") //Was Lui, kan makkelijk vervangen worden door een tag
                    {
                        GameObject g = Instantiate(thingToMake, t.transform.position, Quaternion.identity); //Instantiate het object dat gemaakt moest worden

                        g.transform.SetParent(t); //Set de positie naar midden op het anvil al zie je dat niet
                        g.transform.localPosition = Vector3.zero;//Omdat het object meteen wordt meegenomen naar de storage
                        g.transform.localRotation = t.transform.localRotation;

                        //Zet het gemaakte object zijn parent naar dit object
                        carrying = g; //Zet het object dat wordt gedragen naar het gemaakte object
                        carrying.transform.SetParent(transform);
                        carrying.transform.localPosition = new Vector3(0, 3, 0);
                        carrying.transform.localRotation = transform.localRotation;


                }
                }

                inventory.Clear(); //Leegt de inventory, waarschijnlijk kan dit voor kleine problemen zorgen als er andere type bars of nog ore in de inventory zitten, maar dat heb ik niet getest(En ik heb er geen zin aan na een hele middag nonstop programmeren)
                smeltedOre = false; //Reset deze bools
                coolingDown = false;
                SetState(States.BringResource); //Breng het gemaakte object naar de storage
            }
            else if(amountInInv != thingToMake.GetComponent<Weapon>().amounOfBarsNeeded) //Als er niet genoeg bars zijn, ga idlen
            {
                SetState(States.Idle);
            }
      
        
        else //Als er geen bars in de inventory zijn ga idlen
        {
            SetState(States.Idle);
        }
        
    }

    void Smelt() //Verandert ore in bars
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= 2) //Als het object dichtbij de furnace is
        {
            foreach (string s in inventory) //Loop door de inventory
            {
                if (s.Contains("Ore")) //Als s een ore is
                {
                    inventory[inventory.IndexOf(s)] = thingToMake.GetComponent<Bar>().barType; //Zet de ore naar een bar
                }
            }

            SetThingToMake(cm.allWeapons[0]); //Zet het object om te maken naar een zwaard
            smeltedOre = true; //Reset bools
            hasOre = false;
        }
    }

    IEnumerator CoolDown() //Zorgt ervoor dat niet alles meerdere keren of tegelijk wordt uitgevoerd
    {
        yield return new WaitForSeconds(doJobCooldown);
        coolingDown = false;
    }
   
}
