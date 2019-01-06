using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed;
    Vector3 go = new Vector3();
    public GameObject FOV;

    // Use this for initialization
    void Start() {

        speed = 5;
        reborn();
    }

    // Update is called once per frame
    void Update() {

        go = new Vector3(speed, 0, 0);
        transform.Translate(go * Time.deltaTime);

        if (transform.position.x > -3.85 && transform.position.x < 3.85)
        {
            FOV.SetActive(false);


        }
        else
        {
            FOV.SetActive(true);
        }


        if (transform.position.x > -3.85 && transform.position.x < -3.75 && transform.forward.z == 1)

        {
            bool x = Randomb();
           // Debug.Log(getRangeNum + "asdsada");
            if (getRangeNum % 3 == 0)
                transform.Rotate(0, 180, 0);
            else if ((getRangeNum % 3 == 1))
                transform.Translate(go*10*Time.deltaTime);
            else if ((getRangeNum % 3 ==2))
                reborn();
        }

        if (transform.position.x <3.85 && transform.position.x > 3.75 && transform.forward.z == -1)

        {
            bool x = Randomb();
          //  Debug.Log(getRangeNum+"asdsada");
            if (getRangeNum % 3 == 0)
                transform.Rotate(0, 180, 0);
            else if ((getRangeNum % 3 == 1))
                transform.Translate(go * 10 * Time.deltaTime);
            else if((getRangeNum % 3 == 2))
                reborn();
        }

        if (transform.position.x > 43  && transform.forward.z == 1)
            transform.Rotate(0, 180, 0);
        if (transform.position.x <-43 && transform.forward.z == -1)
            transform.Rotate(0, 180, 0);




        //  if ()

        // if (transform.position.x < 3.3 && transform.forward.z == -1)

        //    transform.Rotate(0, 180, 0);

    }

   public void reborn() {

        bool x = Randomb();
        if (x)
        {
            transform.position = new Vector3(-43, transform.position.y, transform.position.z);
            transform.forward = new Vector3(0, 0, 1);
        }
        else
        {
            transform.position = new Vector3(43, transform.position.y, transform.position.z);
            transform.forward = new Vector3(0, 0, -1);
        }
    }

    private int getRangeNum = 0;
    private int rangeRadomNum = 0;

    bool Randomb()
    {
        do
        {
            rangeRadomNum = UnityEngine.Random.Range(1, 60);
        }
        while (getRangeNum == rangeRadomNum);
        getRangeNum = rangeRadomNum;
        //Debug.Log(getRangeNum);
        if (getRangeNum < 30.5)
            return true;
        else
            return false;
    }






}
