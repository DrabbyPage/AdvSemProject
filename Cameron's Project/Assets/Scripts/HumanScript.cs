using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    // give feedback on call time

    public bool panicMode = false;
    bool hasGun = false;
    public bool beingAttacked = false;
    bool canCheckCondition = true;
    bool canMove = true;
    bool checkForStop = true;
    bool targetSighted = false;
    bool ableToAttack = true;

    GameObject threat;
    GameObject closestBooth;
    GameObject closestChest;
    GameObject target;
    GameObject GameMan;
    GameObject bullet;

    public Vector2 newPoint;
    public Vector2 threatsKnownLoc;

    float turnSpeed = 10;
    float moveSpeed = 30f;
    float targetRadius = 0.2f;
    float shootingDist = 15.0f;

    int runOrArm = 0;

	// Use this for initialization
	void Start ()
    {
        RandomizeMovePoint();
        GameMan = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
        LookAtPoint();
        CheckSituation();
	}

    void CheckSituation()
    {
        beingAttacked = GetComponent<BeingAttackedScript>().GetBeingAttacked();

        if (!beingAttacked)
        {
            if(panicMode && !hasGun)
            {
                CheckCondition();
            }
            else if(hasGun)
            {
                if (targetSighted)
                {
                    Aim();
                }
                else
                {
                    MoveToPoint();
                }

            }
            else
            {
                target = null;

                if(canMove)
                {
                    MoveToPoint();
                }
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }
        
    }

    void CheckCondition()
    {
        if (canCheckCondition)
        {
            runOrArm = Random.Range(0, 100) % 2;
        }

        if (runOrArm == 1)
        {
            RunToBooth();
        }
        else if (runOrArm == 0)
        {
            RunToChest();
        }

        canCheckCondition = false;
    }

    void RunToBooth()
    {
        float dist;

        if(closestBooth == null)
        {
            closestBooth = GameMan.GetComponent<GameManagerScript>().ClosestBooth(gameObject.transform.position);

            if(closestBooth == null)
            {
                runOrArm = 0;
            }
            return;
        }
        else
        {
            target = closestBooth;

            float closestBoothX = closestBooth.transform.position.x;
            float closestBoothY = closestBooth.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - closestBoothX, 2) + Mathf.Pow(transform.position.y - closestBoothY, 2));

            if (dist > 0.4f)
            {
                if (canMove)
                {
                    MoveToPoint();
                }
            }
            else
            {
                canMove = false;

                GetComponent<Animator>().SetBool("Walking", false);

                closestBooth.GetComponent<PhoneBoothScript>().SetOccupation(true);

                StartCoroutine(WaitForCall());
            }
        }

    }

    void RunToChest()
    {
        float dist;

        if (closestChest == null)
        {
            closestChest = GameMan.GetComponent<GameManagerScript>().ClosestChest(gameObject.transform.position);

            if(closestChest == null)
            {
                runOrArm = 1;
            }
            return;
        }
        else
        {
            target = closestChest;

            float closestChestX = closestChest.transform.position.x;
            float closestChestY = closestChest.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - closestChestX, 2) + Mathf.Pow(transform.position.y - closestChestY, 2));

            if (dist > 0.4f)
            {
                MoveToPoint();
            }
            else
            {
                GetComponent<Animator>().SetBool("Walking", false);

                closestChest.GetComponent<ChestScript>().ChangeUse(true);

                StartCoroutine(WaitForItem());
            }
        }
    }

    void MoveToPoint()
    {
        float distance;

        if (target != null)
        {
            newPoint = target.transform.position;
        }

        distance = Mathf.Sqrt(Mathf.Pow(newPoint.x - transform.position.x, 2) + Mathf.Pow(newPoint.y - transform.position.y, 2));

        if (distance < targetRadius)
        {
            GetComponent<Animator>().SetBool("Walking", false);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            //Debug.Log("at the location");
            RandomizeMovePoint();
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", true);

            if (checkForStop)
            {
                StartCoroutine(CheckForNotMoving());

            }

            GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed);
        }

    }

    void LookAtPoint()
    {
        float lookAngle;
        float lookAngleDeg;
        float currAngle = transform.eulerAngles.z;

        float lookX;
        float lookY;

        float diff;
        float yDiff;
        float xDiff;

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

        xDiff = lookX - transform.position.x;
        yDiff = lookY - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        lookAngleDeg = lookAngle * Mathf.Rad2Deg;

        diff = lookAngleDeg - currAngle;

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

    void RandomizeMovePoint()
    {
        float randX;
        float randY;
        float wanderArea = 5.0f;

        randX = Random.Range(-wanderArea, wanderArea);
        randY = Random.Range(-wanderArea, wanderArea);

        newPoint = new Vector2(gameObject.transform.position.x + randX, gameObject.transform.position.y + randY);
    }

    void Aim()
    {
        if (target != null)
        {
            Debug.Log("aiming at zombie");
            // calculate angle between the character and the object
            float lookAngle;
            float prevAngle = transform.eulerAngles.z;

            float xDiff = target.transform.position.x - transform.position.x;
            float yDiff = target.transform.position.y - transform.position.y;

            float dist = Mathf.Sqrt((xDiff * xDiff) + (yDiff * yDiff));

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            float lookAngleDeg = (lookAngle * Mathf.Rad2Deg);

            float diff = lookAngleDeg - prevAngle;

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

            if (diff > 5)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diff < -5)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                if (dist < shootingDist)
                {
                    if (ableToAttack)
                    {
                        if (bullet == null && ableToAttack)
                        {
                            Shoot();
                            StartCoroutine(TimeBetweenShots());
                        }
                    }
                    else
                    {
                        StartCoroutine(TimeBetweenShots());
                    }
                }
                else
                {
                    targetSighted = false;
                }

            }
        }

    }

    void Shoot()
    {
        // calculate angle between the character and the object
        float lookAngle;

        float newX, newY;

        float xDiff = target.transform.position.x - transform.position.x;
        float yDiff = target.transform.position.y - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = (lookAngle * Mathf.Rad2Deg) - 90;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        newX = gameObject.transform.position.x;
        newY = gameObject.transform.position.y;

        Debug.Log("spawning new bullet");

        bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
        bullet.GetComponent<BulletScript>().SetAngle(lookAngleDeg);
        bullet.transform.position = new Vector2(newX, newY);

        ableToAttack = false;
    }

    IEnumerator CheckForNotMoving()
    {
        checkForStop = false;

        yield return new WaitForSeconds(0.2f);

        if (GetComponent<Rigidbody2D>().velocity.magnitude <= 0.9f)
        {
            RandomizeMovePoint();
        }
        checkForStop = true;
    }

    IEnumerator WaitForCall()
    {
        yield return new WaitForSeconds(5.0f);

        if(!beingAttacked)
        {
            if (closestBooth != null)
            {
                // call the police to (the last known loc of the threat)
                if (threat != null)
                {

                    closestBooth.GetComponent<PhoneBoothScript>().CallThePoPo(threatsKnownLoc);
                }
            }


            canCheckCondition = true;
            panicMode = false;
            canMove = true;
        }

    }

    IEnumerator WaitForItem()
    {
        yield return new WaitForSeconds(3.0f);

        hasGun = true;
        canCheckCondition = true;
        panicMode = false;
        canMove = true;

        target = null;

        newPoint = threatsKnownLoc;
    }

    IEnumerator TimeBetweenShots()
    {
        ableToAttack = false;
        yield return new WaitForSeconds(0.75f);
        ableToAttack = true;
    }

    public void SetPanic(bool newMode)
    {
        if(hasGun)
        {
            targetSighted = newMode;
        }
        else
        {
            panicMode = newMode;
        }
    }

    public void SetThreatLocation(GameObject newThreat)
    {
        // this will be used for calling the police or going back to the loc when armed
        threat = newThreat;
        threatsKnownLoc = threat.transform.position;

        if(hasGun)
        {
            target = newThreat;
        }
    }
}
