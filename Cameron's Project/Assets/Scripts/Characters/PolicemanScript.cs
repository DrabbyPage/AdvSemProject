using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : MonoBehaviour
{
    public bool hasGun = true;
    public bool beingAttacked = false;
    public bool targetSighted = false;
    public bool canMove = true;

    GameObject target;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        CheckSituation();
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
        target = newObj;
        GetComponent<MoveScript>().SetTarget(newObj);
    }

    public void SetTargetLastLoc(Vector2 targetLoc)
    {
        GetComponent<MoveScript>().SetMoveVec2(targetLoc);
    }

}
