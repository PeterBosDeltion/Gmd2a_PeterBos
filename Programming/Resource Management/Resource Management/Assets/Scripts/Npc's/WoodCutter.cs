using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutter : Npc {
    private bool coolingDown;
    // Use this for initialization
    void Start() {

        GetMyComponents(); //Woodcutter is letterlijk een kopie van de miner met een paar andere tags en variables om te kijken of ik het makkelijk kon converten
        SetState(States.Gather);
        SetTarget("Tree");

    }

    // Update is called once per frame
    void Update()
    {
        DoJob();
    }

    public override void DoJob() //In dit script zorgt dit ervoor dat het object waar dit script op zit op zoek gaat naar Iron ore, het mijnt en het terug brengt naar de blacksmith
    {
        //throw new NotImplementedException();
        if (state == States.Gather && target != null) //Als dit object een target heeft gaat hij daar naar toe
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 2) //Als het object binnen bereik is Begint het met minen
            {
                if (!coolingDown)
                {
                    StartCoroutine(Mine());

                }
            }
        }
        else if (state == States.Gather && target == null) //Als er geen target is zoekt dit object er eentje op
        {
            SetTarget("Tree");
            if (target == null) //Als er geen targets zijn
            {
                if(inventory.Count <= 0) //Als de inventory van dit object leeg is gaat het idlen
                {
                    SetState(States.Idle);
                }
                else
                {
                    SetState(States.BringResource); //Anders breng het object zijn ore naar de smith
                }
            }
        }
        else if (state == States.BringResource)
        {
            if (target == null)
            {
                SetTarget("SmithChest"); //Zet de target naar de dichtsbijzijnde smith chest
            }
            else if (Vector3.Distance(transform.position, target.transform.position) <= 2) //Als dit object dichtbij genoeg is
            {
                Chest chest = target.GetComponent<Chest>(); //Pakt de chest uit de target
                foreach (string g in inventory)
                {
                    chest.contents.Add(g); //Voegt zijn inventory to aan de kist
                }
                inventory.Clear(); // Leegt de inventory


                target = null; //Reset target

                SetState(States.Gather); //En ga weer minen
            }

        }
        else if(state == States.Idle) //Als er niets te doen is gaat het object idlen
        {
            if (!idling)
            {
                StartCoroutine(Idle());
            }
        }

    }


    IEnumerator Idle()
    {
        idling = true;
        yield return new WaitForSeconds(idleTime);
        SetState(States.Gather); //Nadat het object een tijd heeft gewacht checkt hij of er weer iets te minen is, anders gaat het weer idlen
        idling = false;

    }

    IEnumerator Mine() //Mijnt de ore
    {
        if(target != null)
        {
            coolingDown = true; //Zorgt ervoor dat er niet duizenden Corountines worden gestart
            yield return new WaitForSeconds(doJobCooldown);

            if(target != null)
            {
                Tree tree = target.GetComponent<Tree>();
                if (tree != null)
                {
                    inventory.Add(tree.GetComponent<Resource>().resourcetype); //Add de geminede ore aan de inventory
                }
            }

            if(target.tag.Contains("Tree"))
            {
                Destroy(target.gameObject); //Vernietigd de ore
            }

            if (inventory.Count < maxInvSize) //Als er nog ruimte is in de inventory gaat het object verder met minen
            {
                coolingDown = false;
            }
            
            else //Anders brengt het zijn inventory naar de dichtsbijzijnde smith
            {
                SetState(States.BringResource);
                coolingDown = false;
            }

        }
       
    }

   
}
