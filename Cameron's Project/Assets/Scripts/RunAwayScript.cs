using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayScript : MonoBehaviour
{
    public bool panicMode = false;
    public bool beingAttacked = false;

    //GameObject threat;
    GameObject closestBooth;
    GameObject GameMan;

    Vector2 newPoint;

    float charSpeed = 0.2f;
    float turnSpeed = 10;

	// Use this for initialization
	void Start ()
    {
        GameMan = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckSituation();
	}

    void CheckSituation()
    {
        beingAttacked = GetComponent<BeingAttackedScript>().GetBeingAttacked();

        if (!beingAttacked)
        {
            if (panicMode)
            {
                //RunAwayFromObject(threat);
                RunToBooth();
            }
        }
        else
        {
            GetComponent<WanderingScript>().SetMoving(false);
            StartCoroutine(GetComponent<BeingAttackedScript>().BeingAttacked());
        }
        
    }

    void RunToBooth()
    {
        float dist;
        closestBooth = GameMan.GetComponent<GameManagerScript>().ClosestBooth(gameObject.transform.position);

        if(closestBooth == null)
        {
            // run away

            return;
        }

        float closestBoothX = closestBooth.transform.position.x;
        float closestBoothY = closestBooth.transform.position.y;

        dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - closestBoothX, 2) + Mathf.Pow(transform.position.y - closestBoothY, 2));

        if (dist > 0.4f)
        {
            // calculate angle between the character and the object
            float lookAngle;
            float prevAngle = transform.eulerAngles.z;

            float xDiff = closestBoothX - transform.position.x;
            float yDiff = closestBoothY - transform.position.y;

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            float lookAngleDeg = lookAngle * Mathf.Rad2Deg;


            float diffAngle = lookAngleDeg - prevAngle;

            if (lookAngleDeg < 0)
            {
                lookAngleDeg = lookAngleDeg + 360;
            }

            if (diffAngle > 180)
            {
                diffAngle -= 360;
            }
            else if (diffAngle < -180)
            {
                diffAngle += 360;
            }

            if (diffAngle > 9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diffAngle < -9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                transform.position = MoveToPoint(transform.eulerAngles.z);
            }

        }
        else
        {
            closestBooth.GetComponent<PhoneBoothScript>().SetOccupation(true);
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
        {
            //runAwayLoc = new Vector2(transform.position.x, transform.position.y);
            //runAwayLoc = GameMan.GetComponent<GameManagerScript>().ClosestBooth(gameObject.transform.position).transform.position;
        }
    }

    public void SetThreat(GameObject newThreat)
    {
        //threat = newThreat;
    }
}
