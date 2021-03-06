﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public int bound = 50; // distance from edge scrolling starts
    public int speed = 5;
    int maxSpeed = 20;

    float screenHeight = Screen.height;
    float screenWidth = Screen.width;

    bool canMove;

    [SerializeField]
    bool mouseToEdgeMovement = true;

    FollowPlayerScript folPlayScript;

    // Use this for initialization
    void Start()
    {
        folPlayScript = gameObject.GetComponent<FollowPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = maxSpeed * (int)GetComponent<Camera>().orthographicSize/12;

        CheckMove();

        if(canMove)
        {
            MoveCamera();
        }
    }

    void CheckMove()
    {
        bool lockedToPlayer = folPlayScript.getFollow();

        if(lockedToPlayer)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    void MoveCamera()
    {
        float newX = gameObject.transform.position.x;
        float newY = gameObject.transform.position.y;

        float xSpeed = 0;
        float ySpeed = 0;

        if (mouseToEdgeMovement)
        {
            // move the camera horizontally with mouse pos
            if (Input.mousePosition.x > screenWidth - bound)
            {
                xSpeed = speed;
            }
            if (Input.mousePosition.x < 0 + bound)
            {
                xSpeed = -speed;
            }

            // move the camera vertically with mouse pos 
            if (Input.mousePosition.y > screenHeight - bound)
            {
                ySpeed = speed;
            }
            if (Input.mousePosition.y < 0 + bound)
            {
                ySpeed = -speed;
            }
        }
      
        

        // keyboard camera movement horizontally
        if (Input.GetKey(KeyCode.A))
        {
            xSpeed = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = speed;
        }

        // keyboard camera movement vertically
        if (Input.GetKey(KeyCode.S))
        {
            ySpeed = -speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            ySpeed = speed;
        }

        newX = transform.position.x + xSpeed * Time.deltaTime;
        newY = transform.position.y + ySpeed * Time.deltaTime;

        gameObject.transform.position = new Vector3(newX, newY, gameObject.transform.position.z);
    }

    void MoveTheCamera()
    {

    }
}
