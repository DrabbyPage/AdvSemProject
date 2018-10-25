using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneBoothScript : MonoBehaviour
{
    GameObject GameMan;
    GameObject timerBar;
    public bool boothInUse;

    Image progCircle;

    float time;
    float timeAmount = 5.0f;


	// Use this for initialization
	void Start ()
    {
        GameMan = GameObject.Find("GameManager");
        timerBar = gameObject.transform.GetChild(0).gameObject;
        boothInUse = false;
        time = timeAmount;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(boothInUse)
        {
            StartCallBar();
        }
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Human")
        {
            float dist;
            float distX, distY;

            distX = col.gameObject.transform.position.x - gameObject.transform.position.x;
            distY = col.gameObject.transform.position.y - gameObject.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist > 1.0f)
            {
                boothInUse = false;
            }
            else
            {
                boothInUse = true;
            }
        }
    }

    public void StartCallBar()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
            timerBar.transform.localScale = new Vector3(time / 5, timerBar.transform.localScale.y, timerBar.transform.localScale.z);
        }
        else
        {
            boothInUse = false;
        }
    }

    public void CallThePoPo(Vector2 targetPos)
    {
        GameObject closePoPo;
        closePoPo = GameMan.GetComponent<GameManagerScript>().ClosestPoliceman(targetPos);

        if(closePoPo != null)
        {
            closePoPo.GetComponent<PolicemanScript>().CallInTarget(targetPos);
        }
        else
        {

        }
    }

    public void SetOccupation(bool newBool)
    {
        boothInUse = newBool;
    }
    
}
