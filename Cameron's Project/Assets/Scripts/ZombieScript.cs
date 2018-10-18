﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    float zombieSpeed = 0.05f;
    float turnSpeed = 7.0f;

    Vector2 newPoint;

    bool attacking = false;

	// Use this for initialization
	void Start ()
    {
        newPoint = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update ()
    {
        TurnToClosestHuman();
        CheckToAttack();
	}

    void TurnToClosestHuman()
    {
        GameObject GameMan = GameObject.Find("GameManager");
        GameObject target = GameMan.GetComponent<GameManagerScript>().FindClosestHuman(gameObject);

        if (target != null)
        {
            // calculate angle between the character and the object
            float lookAngle;
            float prevAngle = transform.eulerAngles.z;

            float xDiff = target.transform.position.x - transform.position.x;
            float yDiff = target.transform.position.y - transform.position.y;

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

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
               

            if (diff > 9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diff < -9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                if (!attacking)
                {
                    transform.position = MoveToPoint(transform.eulerAngles.z);
                }
            }
        }
    }

    Vector2 MoveToPoint(float degree)
    {
        float xPoint, yPoint, radians;

        radians = degree * Mathf.Deg2Rad;

        xPoint = Mathf.Cos(radians);
        yPoint = Mathf.Sin(radians);

        newPoint = new Vector2(transform.position.x + (xPoint * zombieSpeed), transform.position.y + (yPoint * zombieSpeed));

        return newPoint;
    }

    void CheckToAttack()
    {
        GameObject GameMan = GameObject.Find("GameManager");
        GameObject target = GameMan.GetComponent<GameManagerScript>().FindClosestHuman(gameObject);

        if (target != null)
        {
            float distX, distY;
            distX = target.transform.position.x - gameObject.transform.position.x;
            distY = target.transform.position.y - gameObject.transform.position.y;

            float dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist < 1.0f)
            {
                attacking = true;
                target.GetComponent<BeingAttackedScript>().SetBeingAttacked(true);

                // activating particle system
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                attacking = false;
                // shutting off particle system
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void ConvertToPlayer()
    {
        GameObject newPlayerObj;

        newPlayerObj = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;

        newPlayerObj.name = "Player";
        newPlayerObj.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        GameObject.Find("Main Camera").GetComponent<FollowPlayerScript>().UpdatePlayerStatus(newPlayerObj);

        GameObject.Find("GameManager").GetComponent<GameManagerScript>().DeleteZombieFromList(gameObject);

        Destroy(gameObject);
    }
}
