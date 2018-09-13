﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {

    float zombieSpeed = 0.2f;
    float turnSpeed = 7.0f;

    Vector2 newPoint;

	// Use this for initialization
	void Start () {
        newPoint = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        MoveToClosestHuman();	
	}

    void MoveToClosestHuman()
    {
        GameObject GameMan = GameObject.Find("GameManager");
        GameObject target = GameMan.GetComponent<GameManagerScript>().FindClosestHuman(gameObject);

        // calculate angle between the character and the object
        float lookAngle;
        float prevAngle = transform.eulerAngles.z;

        float xDiff = target.transform.position.x - transform.position.x;
        float yDiff = target.transform.position.y - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

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
            //transform.position = MoveToPoint(transform.eulerAngles.z);
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
}
