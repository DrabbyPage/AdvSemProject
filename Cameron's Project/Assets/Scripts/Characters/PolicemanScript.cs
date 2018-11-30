using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : MonoBehaviour
{
    GameObject GameMan;

    public bool hasGun = true;
    public bool beingAttacked = false;
    public bool targetSighted = false;
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
                CheckSituation();
            }
        }
        else
        {
            canMove = true;
            CheckSituation();
        }

    }

    void CheckSituation()
    {
        if(canMove)
        {
            GetComponent<MoveScript>().MoveToPoint();
        }
    }

    public void SetGun(bool newGun)
    {
        hasGun = newGun;
    }

    public void SetMoveBool(bool newMove)
    {
        canMove = newMove;
        GetComponent<MoveScript>().SetMoveBool(newMove);
    }

    public void SetBeingAttacked(bool newAttack)
    {
        beingAttacked = newAttack;
    }

    public void SetTargetSighted(bool newSighting)
    {
        targetSighted = newSighting;
    }

    public void CallInTarget(Vector2 targetPoint)
    {
        Debug.Log("threat was called in");
        GetComponent<MoveScript>().SetMoveVec2(targetPoint);
    }

    public void SetTarget(GameObject newObj)
    {
        GetComponent<MoveScript>().SetTarget(newObj);
    }

    public void SetTargetLastLoc(Vector2 targetLoc)
    {
        GetComponent<MoveScript>().SetMoveVec2(targetLoc);
    }

}
