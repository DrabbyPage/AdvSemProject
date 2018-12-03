using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestTargetScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CheckState();
	}

    void CheckState()
    {
        if (gameObject.tag == "Zombie")
        {
            if (GetComponent<MoveScript>().target == null)
            {
                GetClosestTarget();
            }
        }
    }

    void GetClosestTarget()
    {
        GameObject GameMan = GameObject.Find("GameManager");
        GameObject newTarget;

        if (GameMan != null)
        {
            newTarget = GameMan.GetComponent<GameManagerScript>().FindClosestHuman(gameObject);
        }
        else
        {
            newTarget = null;
        }

        if (gameObject.tag == "Zombie")
        {
            GetComponent<MoveScript>().SetTarget(newTarget);
            //GetComponent<ZombieScript>().SetMovePoint(newTarget.transform.position);
        }
    }

    public GameObject GetClosestTargetObject()
    {
        GameObject GameMan = GameObject.Find("GameManager");
        GameObject newTarget;

        if (GameMan != null)
        {
            newTarget = GameMan.GetComponent<GameManagerScript>().FindClosestHuman(gameObject);
        }
        else
        {
            newTarget = null;
        }

        return newTarget;
    }
}
