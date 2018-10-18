using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    public bool panicMode = false;
    public bool hasGun = false;
    public bool beingAttacked = false;

    //GameObject threat;
    GameObject closestBooth;
    GameObject closestChest;
    GameObject target;
    GameObject GameMan;

    Vector2 newPoint;

    float charSpeed = 0.2f;
    float turnSpeed = 10;
    float moveSpeed = 70f;
    float targetRadius = 0.4f;

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
                int runOrArm = Random.Range(0, 100) % 2;

                if (runOrArm == 1)
                {
                    Debug.Log("runnning");
                    RunToBooth();
                }
                else if (runOrArm == 0)
                {
                    Debug.Log("looting");
                    RunToChest();
                }
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
            target = null;
            return;
        }
        else
        {
            target = closestBooth;
        }

        float closestBoothX = closestBooth.transform.position.x;
        float closestBoothY = closestBooth.transform.position.y;

        dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - closestBoothX, 2) + Mathf.Pow(transform.position.y - closestBoothY, 2));

        if (dist > 0.4f)
        {
            MoveToPoint();
        }
        else
        {
            closestBooth.GetComponent<PhoneBoothScript>().SetOccupation(true);
            panicMode = false;
        }


    }

    void RunToChest()
    {
        float dist;
        closestChest = GameMan.GetComponent<GameManagerScript>().ClosestChest(gameObject.transform.position);

        if (closestChest == null)
        {
            target = null;
            return;
        }
        else
        {
            target = closestChest;
        }

        float closestChestX = closestChest.transform.position.x;
        float closestChestY = closestChest.transform.position.y;

        dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - closestChestX, 2) + Mathf.Pow(transform.position.y - closestChestY, 2));

        if (dist > 0.4f)
        {
            MoveToPoint();
        }
        else
        {
            closestChest.GetComponent<ChestScript>().ChangeUse(true);
            panicMode = false;
        }


    }

    void MoveToPoint()
    {
        if (target != null)
        {
            newPoint = target.transform.position;
        }

        float distance = Mathf.Sqrt(Mathf.Pow(newPoint.x - transform.position.x, 2) + Mathf.Pow(newPoint.y - transform.position.y, 2));

        if (distance < targetRadius)
        {
            newPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed);
        }

    }

    void LookAtPoint()
    {
        float lookAngle;
        float currAngle = transform.eulerAngles.z;

        float lookX;
        float lookY;

        if (target != null)
        {
            lookX = target.transform.position.x;
            lookY = target.transform.position.y;
        }
        else
        {
            lookX = newPoint.x;
            lookY = newPoint.y;
        }

        float xDiff = lookX - transform.position.x;
        float yDiff = lookY - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

        float diff = lookAngleDeg - currAngle;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        if (diff > 180)
        {
            diff -= 360;
        }
        else if (diff < -180)
        {
            diff += 360;
        }

        if (diff > 10)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
        }
        else if (diff < -10)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
        }
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
