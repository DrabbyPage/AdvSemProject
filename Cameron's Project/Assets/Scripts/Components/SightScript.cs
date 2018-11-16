using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightScript : MonoBehaviour
{
    bool panicking;
    bool canCheck = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForPanic();
	}

    void CheckForPanic()
    {
        if (gameObject.tag == "Human")
        {
            panicking = gameObject.GetComponent<HumanScript>().panicMode;
        }
        else
        {
            panicking = false;
        }

        if (panicking)
        {
            canCheck = false;
        }
        else
        {
            canCheck = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        bool ableToSee = true;
        RaycastHit hit;

        if (Physics.Linecast(transform.position, collision.gameObject.transform.position, out hit))
        {
            if (hit.transform.tag == "Player" || hit.transform.tag == "Zombie")
            {
                ableToSee = true;
                Debug.Log("can see play/zombie");
            }
            else
            {
                ableToSee = false;
                Debug.Log("obj in the way of player/zombie");
            }
        }

        if(ableToSee)
        {
            if (collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Player")
            {

                if (canCheck)
                {
                    if (gameObject.tag == "Human")
                    {
                        if (GetComponent<HumanScript>().hasGun)
                        {
                            gameObject.GetComponent<HumanScript>().SetTargetSighted(true);
                            gameObject.GetComponent<HumanScript>().SetTarget(collision.gameObject);
                        }
                        else
                        {
                            gameObject.GetComponent<HumanScript>().SetPanic(true);
                        }
                    }
                    else if (gameObject.tag == "Policeman")
                    {
                        gameObject.GetComponent<PolicemanScript>().SetTargetSighted(true);
                        gameObject.GetComponent<PolicemanScript>().SetTarget(collision.gameObject);
                    }

                    gameObject.GetComponent<ShootScript>().SetThreatLocation(collision.gameObject);

                }
            }
        }

    }
}
