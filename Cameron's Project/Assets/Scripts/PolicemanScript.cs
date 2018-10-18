using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : MonoBehaviour
{
    GameObject target;
    //GameObject GameMan;
    GameObject bullet;


    public Vector2 newPoint;
    public Vector2 threatsKnownLoc;

    float shootingDist = 10.0f;
    float turnSpeed = 10;
    float moveSpeed = 30f;
    float targetRadius = 0.2f;

    bool ableToAttack;
    bool beingAttacked;
    bool targetSighted;
    bool canMove;
    bool checkForStop;
    bool reachedPoint;

    // Use this for initialization
    void Start ()
    {
        //GameMan = GameObject.Find("GameManager");

        ableToAttack = true;
        beingAttacked = false;
        targetSighted = false;
        canMove = true;

    }

    // Update is called once per frame
    void Update ()
    {
        beingAttacked = GetComponent<BeingAttackedScript>().GetBeingAttacked();

        if(!beingAttacked)
        {
            if (targetSighted)
            {
                Aim();
            }
            else
            {
                target = null;

                if (canMove)
                {
                    MoveToPoint();
                }
            }
        }
        else
        {
            ableToAttack = false;

            GetComponent<BeingAttackedScript>().BeingAttacked();
        }

	}

    void Aim()
    {
        if (target != null)
        {
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

            if (diff > 3)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diff < -3)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                if(dist < shootingDist)
                {
                    if (ableToAttack)
                    {
                        if(bullet == null)
                        {
                            Shoot();
                        }
                    }
                    else
                    {
                        StartCoroutine(TimeBetweenShots());
                    }
                }
                else
                {
                    // move towards the target
                    //MoveToTarget();
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

        float lookAngleDeg = (lookAngle * Mathf.Rad2Deg)-90;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        newX = gameObject.transform.position.x;
        newY = gameObject.transform.position.y;

        bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
        bullet.GetComponent<BulletScript>().SetAngle(lookAngleDeg);
        bullet.transform.position = new Vector2(newX, newY);

        ableToAttack = false;
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
            reachedPoint = true;
            //GetComponent<Animator>().SetBool("Walking", false);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            Debug.Log("at the location");
            RandomizeMovePoint();
        }
        else
        {
            //GetComponent<Animator>().SetBool("Walking", true);
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

        reachedPoint = false;
    }

    IEnumerator CheckForNotMoving()
    {
        checkForStop = false;

        yield return new WaitForSeconds(1.0f);

        if (GetComponent<Rigidbody2D>().velocity.magnitude == 0)
        {
            RandomizeMovePoint();
        }
        checkForStop = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Zombie")
        {
            if(target == null)
            {
                Debug.Log("new target for policeman");
                targetSighted = true;
                target = col.gameObject;
            }
        }
    }

    IEnumerator TimeBetweenShots()
    {
        yield return new WaitForSeconds(0.75f);
        ableToAttack = true;
    }

    public void BulletIsGone()
    {
        bullet = null;
    }
}
