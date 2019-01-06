using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HTN : MonoBehaviour {

    public NavMeshAgent aget;


    public Transform player;
    public Transform E1;
    public Transform E2;

    int tele;

    public GameObject Telepoint1;
    public GameObject Telepoint2;



    // Use this for initialization
    void Start () {
        tele = 2;
        aget = gameObject.GetComponent<NavMeshAgent>();

        aget.updatePosition = true;
    }
	
	// Update is called once per frame
	void Update () {
        HTNPlan();
        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);//just avoid a bug
       
    }

    private void HTNPlan()
    {
        choosenearest();

        usetele();
        
        cornerdangerzone();
        middangerzone();

        nopickside();


        }

    private void nopickside()
    { int thisside = 0;
        int anotherside = 0;
        GameObject[] picks = GameObject.FindGameObjectsWithTag("Pick");
        for(int i = 0; i < picks.Length; i++)
            {
            if (picks[i].transform.position.z * transform.position.z > 0)
                thisside++;
            else
                anotherside++;
        }
        if (thisside == 0||(anotherside-thisside>2))
        {
                if (transform.position.z > 0)
                {
                    if (E1.position.x - transform.position.x > 0 && transform.forward.x > 0.7)
                    {
                        if ((transform.position - player.position).magnitude > (transform.position - E1.position).magnitude)
                            Teleport();
                        aget.SetDestination(new Vector3(-41, 2, -2));
                    }
                    else if (E1.position.x - transform.position.x < 0 && transform.forward.x < -0.7)
                    {
                        if((transform.position - player.position).magnitude > (transform.position - E1.position).magnitude)
                        Teleport();
                        aget.SetDestination(new Vector3(41, 2, -2));
                    }

                }
                else {
                    if (E2.position.x - transform.position.x > 0 && transform.forward.x > 0.7)
                    {
                        if ((transform.position - player.position).magnitude > (transform.position - E2.position).magnitude)

                            Teleport();
                        aget.SetDestination(new Vector3(-41, 2, 2));
                    }
                    else if (E2.position.x - transform.position.x < 0 && transform.forward.x < -0.7)
                    {
                        if ((transform.position - player.position).magnitude > (transform.position - E2.position).magnitude)

                            Teleport();
                        aget.SetDestination(new Vector3(41, 2, 2));
                    }

                } 
        }

        
        }

    private void usetele()
    {
        if (transform.position.z<15&& transform.position.z>-15) {
            if (transform.position.z > 2 && transform.forward.z > 0.7 && Math.Abs(transform.position.x) < 25 && (E1.position.x - transform.position.x) > 0 && (E1.position.x - transform.position.x) < 10 && minorside())
                Teleport();
            if (transform.position.z > 2 && transform.forward.z < -0.7 && Math.Abs(transform.position.x) < 25 && (E1.position.x - transform.position.x) < 0 && (E1.position.x - transform.position.x) > -10 && minorside())
                Teleport();
            if (transform.position.z < -2 && transform.forward.z > 0.7 && Math.Abs(transform.position.x) < 25 && (E2.position.x - transform.position.x) > 0 && (E2.position.x - transform.position.x) < 10 && minorside())
                Teleport();
            if (transform.position.z < -2 && transform.forward.z < -0.7 && Math.Abs(transform.position.x) < 25 && (E2.position.x - transform.position.x) < 0 && (E2.position.x - transform.position.x) > -10 && minorside())
                Teleport();
        }
        GameObject[] picks = GameObject.FindGameObjectsWithTag("Pick");
        if (picks.Length < 5 && ((transform.position - player.position).magnitude < (transform.position - E1.position).magnitude) && (transform.position - player.position).magnitude < (transform.position - E2.position).magnitude)
            Teleport();

    }

    private void middangerzone()
    {
        if (transform.position.z > 0 && transform.position.x>3.85&& transform.position.x <12)
        {  if (E1.position.x > -1 && E1.position.x < 3.85 && E1.forward.z > 0)
                aget.SetDestination(new Vector3(14.3f, 2,18.5f));
        }
        if (transform.position.z > 0 && transform.position.x < -3.85 && transform.position.x > -12)
        {
            if (E1.position.x < 1 && E1.position.x > -3.85 && E1.forward.z < 0)
                aget.SetDestination(new Vector3(-14.3f, 2, 18.5f));
        }

        if (transform.position.z < 0 && transform.position.x > 3.85 && transform.position.x < 12)
        {
            if (E2.position.x > -1 && E2.position.x < 3.85 && E2.forward.z > 0)
                aget.SetDestination(new Vector3(14.3f, 2, -18.5f));
        }
        if (transform.position.z < 0 && transform.position.x < -3.85 && transform.position.x > -12)
        {
            if (E2.position.x < 1 && E2.position.x > -3.85 && E2.forward.z < 0)
                aget.SetDestination(new Vector3(-14.3f, 2, -18.5f));
        }






    }


    bool minorside() {
        if (((transform.position - player.position).magnitude < (transform.position - E1.position).magnitude) && (transform.position - player.position).magnitude < (transform.position - E2.position).magnitude)
            return false;
        else
        {
            int thisside = 0;
            int anotherside = 0;
            GameObject[] picks = GameObject.FindGameObjectsWithTag("Pick");
            for (int i = 0; i < picks.Length; i++)
            {
                if (picks[i].transform.position.z * transform.position.z > 0)
                    thisside++;
                else
                    anotherside++;
            }
            if (anotherside > thisside)
                return true;
            else
                return false;
        }
    }
    void choosenearest() {

        float distpick;
        float minimum = 10000;
        int index = 0;
        int index2 = 0;
        GameObject[] picks = GameObject.FindGameObjectsWithTag("Pick");
        for (int i = 0; i < picks.Length; i++)
        {
            distpick = (picks[i].transform.position - transform.position).magnitude;
            if (distpick < minimum)
            {
                minimum = distpick;
                index = i;
            }

        }
        if (picks.Length != 0)
            aget.SetDestination(picks[index].transform.position);
        try
        {
            if ((player.position - picks[index].transform.position).magnitude < (transform.position - picks[index].transform.position).magnitude)
            {
                if (picks.Length > 1)
                {
                    minimum = 5000;
                    for (int i = 0; i < picks.Length; i++)
                    {
                        if (i != index)
                        {
                            distpick = (picks[i].transform.position - transform.position).magnitude;
                            if (distpick < minimum)
                            {
                                minimum = distpick;
                                index2 = i;
                            }
                        }
                    }
                    aget.SetDestination(picks[index2].transform.position);
                }
                else
                    if(((transform.position-player.position).magnitude< (transform.position - E1.position).magnitude)&& (transform.position - player.position).magnitude < (transform.position - E2.position).magnitude)
                    Teleport();

            }
        }
        catch { }
    }
    void Teleport() {
        if (tele >0)
        {
            if (tele == 2)
                Destroy(Telepoint1);
            else
                Destroy(Telepoint2);
            Debug.Log("AIteleport");
            tele -=1;
            float dist, distPL;
            float minimum = 10000;
            int index = 0;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");


            for (int i = 0; i < enemies.Length; i++)
            {
                dist = (enemies[i].transform.position - transform.position).magnitude;
                if (dist < minimum)
                {
                    minimum = dist;
                    index = i;
                }

            }
            try
            {
                distPL = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).magnitude;

                if (distPL < minimum)
                {

                    GameObject.FindGameObjectWithTag("GameController").GetComponent<Judge>().randomset(GameObject.FindGameObjectWithTag("Player"));

                }
                else
                {

                    enemies[index].SendMessage("reborn");
                }
            }
            catch
            { enemies[index].SendMessage("reborn"); }
        }


    }

    void cornerdangerzone() {
        if (transform.position.x > 32 && transform.position.z > 10)
        {
            if(E1.transform.position.x > 25&& E1.transform.forward.z>0)
                aget.SetDestination(new Vector3(41,2,7));
        }

        if (transform.position.x < -32 && transform.position.z > 10)
        {
            if (E1.transform.position.x < -25 && E1.transform.forward.z < 0)
                aget.SetDestination(new Vector3(-41, 2, 7));
        }
        if (transform.position.x >32 && transform.position.z <-10)
        {
            if (E2.transform.position.x >25 && E2.transform.forward.z > 0)
                aget.SetDestination(new Vector3(41, 2, -7));
        }

        if (transform.position.x < -32 && transform.position.z <- 10)
        {
            if (E2.transform.position.x < -25 && E2.transform.forward.z < 0)
                aget.SetDestination(new Vector3(-41, 2, -7));
        }



    }


}




