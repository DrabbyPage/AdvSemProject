using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightScript : MonoBehaviour {

    bool panicking;
    bool canCheck = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        panicking = GetComponent<RunAwayScript>().panicMode;
        if (panicking)
            canCheck = false;
        else
            canCheck = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Player")
        {
            if (canCheck)
            {
                //Debug.Log("PANIC!");
                gameObject.transform.GetComponent<RunAwayScript>().SetThreat(collision.gameObject);
                gameObject.GetComponent<RunAwayScript>().SetPanic(true);
            }
        }
        else
        {
            // you will have to adjust this
            //gameObject.transform.GetComponent<RunAwayScript>().SetPanic(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Player")
        {
            //Debug.Log("PANIC!");
            gameObject.transform.GetComponent<RunAwayScript>().SetThreat(collision.gameObject);
            gameObject.transform.GetComponent<RunAwayScript>().SetPanic(true);
        }
    }
}
