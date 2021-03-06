﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    GameObject GameMan;
    public GameObject closestHuman;

    //bool attacking = false;
    public bool canMove = true;

    // Use this for initialization
    void Start ()
    {
        GameMan = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForPause();

        CheckPath();

        // might change to wander and go to roar
        if(canMove)
        {
            GetComponent<MoveScript>().MoveToPoint();
        }
        
	}

    void CheckPath()
    {
        if (GetComponent<PathHolderScript>().objectPath.Count > 0 || closestHuman != null)
        {
            Vector3 nextNodePos = GetComponent<PathHolderScript>().GetNextPos();

            Vector3 diff = nextNodePos - gameObject.transform.position;

            float dist = diff.magnitude;

            float minDist = 0.5f;

            if(dist < minDist)
            {
                if (GetComponent<PathHolderScript>().objectPath.Count > 0)
                {
                    GetComponent<PathHolderScript>().KnockOutPathNode();

                    nextNodePos = GetComponent<PathHolderScript>().GetNextPos();
                }
            }

            SetMovePoint(nextNodePos);
        }
        else
        {
            closestHuman = GetComponent<FindClosestTargetScript>().GetClosestTargetObject();

            if(closestHuman != null)
            {
                Vector2 closeObjVec2 = closestHuman.transform.position;
                Vector2 charObjVec2 = gameObject.transform.position;

                GetComponent<PathHolderScript>().GeneratePath(charObjVec2, closeObjVec2);
            }
        }
    }

    void CheckForPause()
    {
        if (GameMan != null)
        {
            if (GameMan.GetComponent<GameManagerScript>().gamePaused)
            {
                gameObject.GetComponent<Animator>().SetBool("Walking", false);
                canMove = false;
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Walking", true);
                canMove = true;
            }
        }
        else
        {
            canMove = true;
        }

    }

    public void SetMoveBool(bool newMove)
    {
        canMove = newMove;
    }

    public void SetMovePoint(Vector2 newPos)
    {
        GetComponent<MoveScript>().SetMoveVec2(newPos);
    }

    public void ConvertToPlayer()
    {
        GameObject newPlayerObj;
        GameObject camera = GameObject.Find("Main Camera");
        GameObject GameMan = GameObject.Find("GameManager");

        newPlayerObj = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;

        newPlayerObj.name = "Player";
        newPlayerObj.tag = "Player";
        newPlayerObj.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        camera.GetComponent<FollowPlayerScript>().UpdatePlayerStatus(newPlayerObj);
        camera.GetComponent<OneClickSelectionScript>().SetPlayer(newPlayerObj);

        GameMan.GetComponent<GameManagerScript>().AddZombieToList(newPlayerObj);
        GameMan.GetComponent<GameManagerScript>().DeleteZombieFromList(gameObject);

        Destroy(gameObject);
    }

}
