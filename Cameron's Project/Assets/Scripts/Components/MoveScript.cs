using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10f;

    public float turnSpeed = 10;
    float distToTargetPos = 0.2f;

    public Vector2 walkToPoint;
    public GameObject target;

    public bool canMove = true;
    bool stuck = false;

    float notMovingTimer = 0;
    float maxTime = 3.0f;

    void Start()
    {
        walkToPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        CheckCanMove();

        if (canMove)
        {
            MoveToPoint();
            CheckForNotMoving();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
    }

    void CheckCanMove()
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
    }

    public void MoveToPoint()
    {
        float distance;
        Vector2 playerPos = gameObject.transform.position;
        //Vector2 direction = walkToPoint - playerPos;

        if (target != null)
        {
            walkToPoint = target.transform.position;
        }

        Vector2 direction = walkToPoint - playerPos;

        distance = Mathf.Sqrt(Mathf.Pow(walkToPoint.x - transform.position.x, 2) + Mathf.Pow(walkToPoint.y - transform.position.y, 2));

        if (distance < distToTargetPos)
        {            
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            if (gameObject.tag == "Human" || gameObject.tag == "Policeman")
            {
                gameObject.GetComponent<WanderScript>().RandomizePoint();

            }

            GetComponent<Animator>().SetBool("Walking", false);
        }
        else
        {
            if(stuck)
            {
                if (gameObject.tag == "Human" || gameObject.tag == "Policeman")
                {
                    gameObject.GetComponent<WanderScript>().RandomizePoint();
                }

                stuck = false;

                //GetComponent<Rigidbody2D>().AddForce(direction.normalized * moveSpeed);

            }

            GetComponent<Rigidbody2D>().AddForce(direction.normalized * moveSpeed);

            if(direction.normalized.x > 0.4f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (direction.normalized.x < -0.4f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            GetComponent<Animator>().SetBool("Walking", true);

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
        GetComponent<Animator>().SetBool("Walking", newMove);
    }
}
