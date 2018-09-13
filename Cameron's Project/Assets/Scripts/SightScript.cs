using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Player")
        {
            Debug.Log("PANIC!");
            gameObject.transform.parent.GetComponent<RunAwayScript>().SetThreat(collision.gameObject);
            gameObject.transform.parent.GetComponent<RunAwayScript>().SetPanic(true);
        }
        else
        {
            // you will have to adjust this
            gameObject.transform.parent.GetComponent<RunAwayScript>().SetPanic(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Player")
        {
            Debug.Log("PANIC!");
            gameObject.transform.parent.GetComponent<RunAwayScript>().SetThreat(collision.gameObject);
            gameObject.transform.parent.GetComponent<RunAwayScript>().SetPanic(true);
        }
    }
}
