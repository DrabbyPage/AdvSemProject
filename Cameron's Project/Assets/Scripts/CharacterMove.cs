﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    public GameObject target;
    
    public Vector2 newPoint;

    float moveSpeed = 10.0f;
    float lockOnRange = 0.5f;

    bool ableToMove = true;

    // Use this for initialization
    void Start()
    {
        target = null;
        newPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTargetLife();
        CheckForNewPoint();
        CheckToMove();
    }

    void CheckForTargetLife()
    {
        if (target != null)
        {
            if (target.tag == "Dead")
            {
                target = null;
                ableToMove = true;
            }
        }

    }

    void CheckForNewPoint()
    {
        if (Input.GetMouseButton(0))
        {
            newPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                                                                            Input.mousePosition.y, 
                                                                            gameObject.transform.position.y));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);

            if (target == null)
                CheckHumanDist(mouseWorld);
        }
    }

    void CheckToMove()
    {
        if (ableToMove)
            MoveToPoint();
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            ableToMove = false;
        }
    }

    void MoveToPoint()
    {
        if(target != null)
        {
            newPoint = new Vector2(target.transform.position.x, target.transform.position.y);
        }

        float distance = Mathf.Sqrt(Mathf.Pow(newPoint.x - transform.position.x, 2) + Mathf.Pow(newPoint.y - transform.position.y, 2));

        //Debug.Log(distance);

        if(distance > 0.5f && ableToMove)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed);
            
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            ableToMove = false;
        }
    }

    void CheckHumanDist(Vector3 mouse)
    {
        GameObject newTarget = GameObject.Find("GameManager").GetComponent<GameManagerScript>().CloseToHuman(mouse, lockOnRange);

        if (newTarget != null)
            target = newTarget;
    }

    public void SetMoveAbility(bool newMove)
    {
        ableToMove = newMove;
    }

}
