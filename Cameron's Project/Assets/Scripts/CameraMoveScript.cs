using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public int bound = 50; // distance from edge scrolling starts
    public int speed = 15;

    int screenWidth;
    int screenHeight;

    bool canMove;

    FollowPlayerScript folPlayScript;

    // Use this for initialization
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        folPlayScript = gameObject.GetComponent<FollowPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.mousePosition.x > screenWidth - bound)
        {
            newX = transform.position.x + speed * Time.deltaTime;
        }
        if (Input.mousePosition.x < 0 + bound)
        {
            newX = transform.position.x - speed * Time.deltaTime;
        }

        if (Input.mousePosition.y > screenHeight - bound)
        {
            newY = transform.position.y + speed * Time.deltaTime;
        }
        if (Input.mousePosition.y < 0 + bound)
        {
            newY = transform.position.y - speed * Time.deltaTime;
        }

        gameObject.transform.position = new Vector3(newX, newY, gameObject.transform.position.z);
    }

    void MoveTheCamera()
    {

    }
}
