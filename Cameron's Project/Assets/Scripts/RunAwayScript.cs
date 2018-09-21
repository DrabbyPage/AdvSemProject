using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayScript : MonoBehaviour {

    public bool panicMode = false;
    public bool beingAttacked = false;

    GameObject threat;
    Vector2 runAwayLoc;

    float charSpeed = 0.2f;
    float turnSpeed = 10;

    Vector2 newPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckSituation();
	}

    void CheckSituation()
    {
        if (!beingAttacked)
        {
            if (panicMode)
                RunAwayFromObject(threat);
        }
        else
        {
            StartCoroutine(GetComponent<BeingAttackedScript>().BeingAttacked());
        }
        
    }

    void RunAwayFromObject(GameObject obj)
    {
        // calcualte distance between obj and the character
        float dist;
        dist = Mathf.Sqrt( Mathf.Pow(transform.position.x - runAwayLoc.x, 2) + Mathf.Pow(transform.position.y - runAwayLoc.y, 2) );

        if (dist < 15)
        {
            // calculate angle between the character and the object
            float lookAngle;
            float prevAngle = transform.eulerAngles.z;

            float xDiff = obj.transform.position.x - transform.position.x;
            float yDiff = obj.transform.position.y - transform.position.y;

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            float lookAngleDeg = lookAngle * Mathf.Rad2Deg;
            lookAngleDeg = lookAngleDeg + 180;

            // make character look in the opposite direction

            if (lookAngleDeg > prevAngle + 9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (lookAngleDeg < prevAngle - 9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                // run in the opposite direction
                transform.position = MoveToPoint(transform.eulerAngles.z);

            }

        }
        else
        {
            panicMode = false;
        }
    }

    Vector2 MoveToPoint(float degree)
    {
        float xPoint, yPoint, radians;

        radians = degree * Mathf.Deg2Rad;

        xPoint = Mathf.Cos(radians);
        yPoint = Mathf.Sin(radians);

        newPoint = new Vector2(transform.position.x + (xPoint * charSpeed), transform.position.y + (yPoint * charSpeed)); 

        return newPoint;
    }

    public void SetPanic(bool newMode)
    {
        panicMode = newMode;

        if (panicMode)
            runAwayLoc = new Vector2(transform.position.x, transform.position.y);
    }

    public void SetThreat(GameObject newThreat)
    {
        threat = newThreat;
    }

    public void setBeingAttacked(bool newState)
    {
        beingAttacked = newState;
    }
}
