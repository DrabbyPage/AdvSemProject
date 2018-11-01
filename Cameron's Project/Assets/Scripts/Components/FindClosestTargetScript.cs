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
        CheckState();
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
        GameObject newTarget = GameObject.Find("GameManager").GetComponent<GameManagerScript>().FindClosestHuman(gameObject);

        if(gameObject.tag == "Zombie")
        {
            GetComponent<MoveScript>().SetTarget(newTarget);
        }
    }
}
