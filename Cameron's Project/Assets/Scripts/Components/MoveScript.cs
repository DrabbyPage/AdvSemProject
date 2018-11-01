using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10f;

    public float turnSpeed = 10;
    float distToTargetPos = 0.2f;

    Vector2 walkToPoint;
    public GameObject target;

    public bool canMove = true;
    bool stuck = false;

    float notMovingTimer = 0;
    float maxTime = 2.0f;

    private void Start()
    {
        walkToPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (gameObject.tag == "Human")
        {
            canMove = GetComponent<HumanScript>().canMove;
        }
        else if (gameObject.tag == "Player")
        {
            canMove = GetComponent<CharacterScript>().canMove;
        }
        else if (gameObject.tag == "Zombie")
        {
            canMove = GetComponent<ZombieScript>().canMove;
        }
        else if (gameObject.tag == "Policeman")
        {
            canMove = GetComponent<PolicemanScript>().canMove;
        }


        if (canMove)
        {
            LookAtPoint();
            MoveToPoint();
            CheckForNotMoving();
        }
	}

    public void MoveToPoint()
    {
        float distance;

        if (target != null)
        {
            walkToPoint = target.transform.position;
        }

        distance = Mathf.Sqrt(Mathf.Pow(walkToPoint.x - transform.position.x, 2) + Mathf.Pow(walkToPoint.y - transform.position.y, 2));

        if (distance < distToTargetPos)
        {
            //GetComponent<Animator>().SetBool("Walking", false);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            if (gameObject.tag == "Human" || gameObject.tag == "Policeman")
            {
                gameObject.GetComponent<WanderScript>().RandomizePoint();
            }
        }
        else
        {
            if(stuck)
            {
                if (gameObject.tag == "Human" || gameObject.tag == "Policeman")
                {
                    gameObject.GetComponent<WanderScript>().RandomizePoint();
                }
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
            lookX = walkToPoint.x;
            lookY = walkToPoint.y;
        }

        xDiff = lookX - transform.position.x;
        yDiff = lookY - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        lookAngleDeg = lookAngle * Mathf.Rad2Deg;

        diff = lookAngleDeg - currAngle;

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

    void CheckForNotMoving()
    {
        if (notMovingTimer > 0)
        {
            notMovingTimer = notMovingTimer - Time.deltaTime;
        }
        else
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude <= 1.0f)
            {
                stuck = true;
            }
            else
            {
                stuck = false;
            }
            notMovingTimer = maxTime;
        }
    }

    public void SetMoveVec2(Vector2 newPoint)
    {
        walkToPoint = newPoint;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void SetMoveBool(bool newMove)
    {
        canMove = newMove;
    }
}
