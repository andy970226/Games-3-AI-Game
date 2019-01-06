using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Judge : MonoBehaviour {
   

    public Text winText;

    public GameObject player;
    public GameObject AIplayer;



    void Awake() {


    }
    // Use this for initialization
    void Start () {
        AIplayer.GetComponent<NavMeshAgent>().enabled = false;
        randomset(player);
        randomset(AIplayer);
        AIplayer.GetComponent<NavMeshAgent>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void JudgeWin()
    {
        int plcount = player.GetComponent<Collect>().picks;
        int aicount = AIplayer.GetComponent<AIcollect>().picks;

        bool plalive = player.GetComponent<Collect>().alive;
        bool aialive = AIplayer.GetComponent<AIcollect>().alive;

        if (!plalive && !aialive)
        {
            if (plcount > aicount)
            {
                PLWintext();
                Time.timeScale = 0;
            }
            else if (plcount < aicount)
            {
                AIWintext();
                Time.timeScale = 0;
            }
            else
            {
                TieWintext();
                Time.timeScale = 0;

            }
        }
        else if (plcount + aicount == 10)
        {

            if (plcount > aicount)
            {
                PLWintext();
                Time.timeScale = 0;
            }
            else if (plcount < aicount)
            {
                AIWintext();
                Time.timeScale = 0;
            }
            else
            {
                TieWintext();
                Time.timeScale = 0;

            }

        }



    }

    void PLWintext()
    {
        winText.text = "You Win";

    }
    void AIWintext()
    {
        winText.text = "AI Win";

    }
    void TieWintext()
    {
        winText.text = "Tie Game";

    }


    public void randomset(GameObject obj)

    {
        int i = Random();
        if (i < 5.5)
        {

            
            obj.transform.position = new Vector3(-45 + 15 * i, 2f, 22.7f);

        }
        else
        {
           
            obj.transform.position = new Vector3(-45 + 15 * (i-5), 2f, -22.7f);

        }

    }



    int getRangeNum = 0;
    int rangeRadomNum = 0;

    int Random()
    {
        do
        {
            rangeRadomNum = UnityEngine.Random.Range(1, 10);
        }
        while (getRangeNum == rangeRadomNum);
        getRangeNum = rangeRadomNum;

        return getRangeNum;
    }
}
