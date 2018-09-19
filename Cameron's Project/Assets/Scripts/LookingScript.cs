﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingScript : MonoBehaviour {

    Vector2 newPoint;
    Vector2 mousePos;


    // see zombies in trigger area, draw raycast, see if theres anything in between,
    // if nothing then freak out

    float turnSpeed = 7.0f;

	// Use this for initialization
	void Start () {
        newPoint = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        CheckForNewPoint();
        LookAtPoint();
	}

    void CheckForNewPoint()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("New Point Made");
            newPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.y));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);

            //if()

        }
    }

    void LookAtPoint()
    {
        float lookAngle;
        float currAngle = transform.eulerAngles.z;

        float xDiff = newPoint.x - transform.position.x;
        float yDiff = newPoint.y - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

        float diff = lookAngleDeg - currAngle;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        if (diff > 180)
            diff -= 360;
        else if (diff < -180)
            diff += 360;

        if (diff > 9)
            TurnRight();
        else if (diff < -9)
            TurnLeft();
        else
            MoveToPoint();

    }

    void MoveToPoint()
    {
        GetComponent<CharacterMove>().SetMoveAbility(true);
    }

    void TurnRight()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
        GetComponent<CharacterMove>().SetMoveAbility(false);

    }

    void TurnLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
        GetComponent<CharacterMove>().SetMoveAbility(false);

    }

}
