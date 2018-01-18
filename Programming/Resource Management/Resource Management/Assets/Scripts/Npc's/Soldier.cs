using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Npc {
    private bool cooldown;
    private Weapon myWeapon;
    private Vector3 enemyStartPos;
	// Use this for initialization
	void Start () {
        GetMyComponents();
        SetState(States.Gather);
    }

    // Update is called once per frame
    void Update () {
        Die();
        if (state == States.Gather && !idling) 
        {
            FindWeapon();//Als dit object geen wapen heeft ga er een halen
            if (myTool != null) //Als het een wapen heeft
            {
                myWeapon = myTool.GetComponent<Weapon>();
                SetState(States.Find); //Ga enemies zoeken
            }
            else //Anders ga idlen
            {
                SetState(States.Idle);
            }
         
        }
        else if(state == States.Find && !idling) //Zoek naar de dichtsbijzijnde enemy
        {
            if(target == null)
            {
                GameObject[] allEms = GameObject.FindGameObjectsWithTag("Enemy"); //Kijk of er nog enemies in leven zijn
                if(allEms.Length > 0)
                {
                    SetTarget("Enemy");  //Zet de agents destionation naar de enemy
                    enemyStartPos = target.transform.position; //Zet de start positie van de enemy gelijk aan zijn huidige positie

                }
                else //Als er geen enemies zijn ga idlen
                {
                    SetState(States.Idle);
                }
                
            }
            else if (myWeapon != null) //Als het object een target en een wapen heeft
            {
                if(Vector3.Distance(transform.position, target.transform.position) <= myWeapon.attackRange && target != null && !idling) //Als het dichtbij genoeg de enemy is ga vechten
                {
                    SetState(States.Fight);
                }
                else if (Vector3.Distance(transform.position, target.transform.position) > myWeapon.attackRange && target != null && target.tag == "Enemy") //Als het object te ver is van de enemy
                {
                    if (target.transform.position != enemyStartPos) //En de enemy is niet meer op zijn startpositie
                    {
                        agent.SetDestination(target.transform.position); //Zet de agents destination naar de nieuwe positie van de enemy
                        enemyStartPos = target.transform.position; //En zet de startpos weer gelijk aan de nieuwe positie
                    }
                }
            }
            
        }
        else if(state == States.Fight && target != null && !idling && !cooldown) //Dit doet hetzelfde als hierboven alleen dan in de Fight state
        {
            if(target.transform.position != enemyStartPos && Vector3.Distance(transform.position, target.transform.position) > myWeapon.attackRange)
            {
                agent.SetDestination(target.transform.position);
                enemyStartPos = target.transform.position;
            }
            DoJob(); //Vecht met de enemy
            StartCoroutine(Cooldown()); //Zorgt ervoor dat de enemy niet instant dood gaat
        }
        else if (state == States.Fight && target == null) //Als er geen target meer is ga idlen
        {
            SetState(States.Idle);
        }
        else if (state == States.Idle && !idling) //Idle
        {
            StartCoroutine(Idle());
        }
    }

    

    public void FindWeapon() //Zoekt een storage en pakt er een wapen uit
    {
        if(target == null)
        {
            SetTarget("Storage"); //Zet de agents destination naar een storage
        }
        else if(Vector3.Distance(transform.position, target.transform.position) <= 2) //Als hij dichtbij de storage is
        {
            Storage st = target.GetComponent<Storage>();
            int amountOfWeaponsInStorage = 0; //Check of er wapens in de storage zijn
            foreach (GameObject g in st.contents) //Loop door de storage heen
            {
                if(g.tag == "Weapon" && myTool == null) //Als g een wapen is en dit object heeft geen wapen
                {
                    amountOfWeaponsInStorage++; //Tel 1 bij de aantal wapens op
                    if(amountOfWeaponsInStorage > 0) //Als er wapens zijn
                    {
                        g.transform.SetParent(transform);
                        g.transform.localPosition = new Vector3(0, 3, 0);
                        myTool = g; //Maak dit object zijn tool het gevonden wapen 
                    }
                    else //Als er geen wapens zijn ga Idlen
                    {
                        SetState(States.Idle);
                    }
                   
                }
            }

            st.contents.Remove(myTool); //haal het gevonden wapen uit de storage
            target = null; //Reset de target
        }
    }

    public override void DoJob() //Vechten met de enemy
    {
        //throw new NotImplementedException();
        Enemy enm = target.GetComponent<Enemy>();
        enm.SetState(Enemy.States.Fight); //Laat de enemy terugvechten
        if(enm.target == null) //Als de enemy nog geen target heeft maak dit object zijn target
        {
            enm.target = gameObject;
        }

        float crit = Random.value; //Bereken of het object critical damage doet
        if(crit < myWeapon.critChance) //Zo ja doe damage * 3
        {
            int f = Random.Range(myWeapon.minDamage, myWeapon.maxDamage) * myWeapon.critMultiplier;
            enm.health -= f;
        }
        else                //Anders doe gewoon damage
        {
            int f = Random.Range(myWeapon.minDamage, myWeapon.maxDamage);
            enm.health -= f;
        }

    }

    IEnumerator Idle() //Idle
    {
        if (hp > 100) //Als het object meer dan 100 hp heeft maak het 100(zou eigenlijk in een maxhealth variable moeten)
        {
            hp = 100;
        }
        idling = true;
        yield return new WaitForSeconds(idleTime);
        if(hp < 100) //Als mijn hp lager dan 100 is heal een klein beetje
        {
            hp += 20;
        }
       
        if(myTool == null) //Als het object geen wapen heeft ga er een zoeken
        {
            SetState(States.Gather);
        }
        else //Anders ga enemies zoeken
        {
            SetState(States.Find);
        }
        idling = false;
    }

    IEnumerator Cooldown() //Zorg ervoor dat enemies niet instant dood gaat
    {
        cooldown = true;
        yield return new WaitForSeconds(doJobCooldown);
        cooldown = false;
    }


}
