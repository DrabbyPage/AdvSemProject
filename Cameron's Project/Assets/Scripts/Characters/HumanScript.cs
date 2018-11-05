using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    // this is the manager for the human script
    // and all the scripts that may be used by the human

    public bool panicMode = false;
    public bool hasGun = false;
    public bool beingAttacked = false;
    public bool targetSighted = false;
    public bool canMove = true;
	
	// Update is called once per frame
	void Update ()
    {
        CheckSituation();
	}

    void CheckSituation()
    {
        if(panicMode && !hasGun)
        {
            GetComponent<RunToObjectScript>().RunToObj();
        }
        else if(!panicMode && canMove)
        {
            GetComponent<MoveScript>().MoveToPoint();
        }
    }

    public void SetPanic(bool newMode)
    {
        panicMode = newMode;
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

    public void SetTarget(GameObject newObj)
    {
        GetComponent<MoveScript>().SetTarget(newObj);
    }

    public void SetTargetLastLoc(Vector2 targetLoc)
    {
        GetComponent<MoveScript>().SetMoveVec2(targetLoc);
    }

}
