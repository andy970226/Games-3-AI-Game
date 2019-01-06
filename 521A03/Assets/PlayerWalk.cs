using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalk : MonoBehaviour {
    public float speed;
    public int Teletrap;
    public GameObject Telepoint1;
    public GameObject Telepoint2;

    Vector3 go = new Vector3();
    // Use this for initialization
    void Start () {

        speed = 10;
        Teletrap = 2;
	}
	
	// Update is called once per frame
	void Update () {
        go = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            go += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            go += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            go += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            go += new Vector3(0, 0, -1);
        }
        transform.Translate(go.normalized*speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E) && Teletrap >0)
        {
            if (Teletrap == 2)
                Destroy(Telepoint1);
            else
                Destroy(Telepoint2);
            Teletrap --;
            float dist,distAI;
            float minimum=10000;
            int index = 0;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            
            for (int i=0;i<enemies.Length;i++)
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
                distAI = (GameObject.FindGameObjectWithTag("AIPlayer").transform.position - transform.position).magnitude;

                if (distAI < minimum)
                {
                    GameObject.FindGameObjectWithTag("AIPlayer").GetComponent<NavMeshAgent>().enabled = false;
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<Judge>().randomset(GameObject.FindGameObjectWithTag("AIPlayer"));
                    GameObject.FindGameObjectWithTag("AIPlayer").GetComponent<NavMeshAgent>().enabled = true;
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

   


}
