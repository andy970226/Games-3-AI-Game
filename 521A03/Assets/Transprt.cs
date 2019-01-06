using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transprt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().material.color = new Color(0.5f, 0.8f, 0.1f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Collect>().alive = false;
            //other.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameController").SendMessage("JudgeWin");
        }

        if (other.gameObject.CompareTag("AIPlayer"))
        {
            GameObject.FindGameObjectWithTag("AIPlayer").GetComponent<AIcollect>().alive = false;
            //other.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("GameController").SendMessage("JudgeWin");
        
    }

    }
}
