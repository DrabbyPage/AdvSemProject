using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayScript : MonoBehaviour {

    bool panicMode = false;

    GameObject threat;

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
        if(panicMode)
        {
            RunAwayFromObject(threat);
        }
    }

    void RunAwayFromObject(GameObject obj)
    {
        // calcualte distance between obj and the character
        float dist;
        dist = Mathf.Sqrt( Mathf.Pow(transform.position.x - obj.transform.position.x, 2) + Mathf.Pow(transform.position.y - obj.transform.position.y, 2) );

        if (dist < 25)
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
    }

    public void SetThreat(GameObject newThreat)
    {
        threat = newThreat;
    }

}
