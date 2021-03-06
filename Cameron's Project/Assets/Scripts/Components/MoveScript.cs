﻿using System.Collections;
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
    float maxTime = 1.0f;

    Vector2 StartPos;

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
        GameObject GameMan = GameObject.Find("GameManager");

        if (gameObject.tag == "Human")
        {
            if (GameMan != null)
            {
                canMove = GetComponent<HumanScript>().canMove && !GameMan.GetComponent<GameManagerScript>().gamePaused;
            }
            else
            {
                canMove = GetComponent<HumanScript>().canMove;
            }
        }
        else if (gameObject.tag == "Player")
        {
            if (GameMan != null)
            {
                canMove = GetComponent<CharacterScript>().canMove && !GetComponent<HealthScript>().isDying && !GameMan.GetComponent<GameManagerScript>().gamePaused;
            }
            else
            {
                canMove = GetComponent<CharacterScript>().canMove && !GetComponent<HealthScript>().isDying;
            }
        }
        else if (gameObject.tag == "Zombie")
        {
            if (GameMan != null)
            {
                canMove = GetComponent<ZombieScript>().canMove && !GetComponent<HealthScript>().isDying && !GameMan.GetComponent<GameManagerScript>().gamePaused;
            }
            else
            {
                canMove = GetComponent<ZombieScript>().canMove && !GetComponent<HealthScript>().isDying;
            }
        }
        else if (gameObject.tag == "Policeman")
        {
            if (GameMan != null)
            {
                canMove = GetComponent<PolicemanScript>().canMove && !GameMan.GetComponent<GameManagerScript>().gamePaused;
            }
            else
            {
                canMove = GetComponent<PolicemanScript>().canMove;
            }
        }

        GetComponent<Animator>().SetBool("Walking", canMove);

    }

    public void MoveToPoint()
    {
        float distance;
        Vector2 playerPos = gameObject.transform.position;

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

            if (gameObject.tag == "Human")
            {
                if(!GetComponent<HumanScript>().inObj)
                {
                    gameObject.GetComponent<WanderScript>().RandomizePoint();
                    StartPos = gameObject.transform.position;
                }
            }
            else if (gameObject.tag == "Policeman")
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

            }

            GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;

            if(direction.normalized.x > 0.4f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (direction.normalized.x < -0.4f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
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
            Vector2 diff = gameObject.transform.position - new Vector3(StartPos.x, StartPos.y, 0);

            float dist = diff.magnitude;

            if (dist <= 1.0f)
            {
                stuck = true;
            }
            else
            {
                stuck = false;
            }
            notMovingTimer = maxTime;

            StartPos = gameObject.transform.position;
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
