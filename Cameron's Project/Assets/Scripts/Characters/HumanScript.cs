using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    // this is the manager for the human script
    // and all the scripts that may be used by the human

    GameObject GameMan;

    public bool panicMode = false;
    public bool hasGun = false;
    public bool beingAttacked = false;
    public bool targetSighted = false;
    public bool canMove = true;
    public bool inObj = false;

    GameObject closestObj; 

    void Start()
    {
        GameMan = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update ()
    {
        CheckForPause();

        CheckForPath();
        
	}

    void CheckForPath()
    {
        if(panicMode && !inObj)
        {
            if(GetComponent<PathHolderScript>().objectPath.Count > 0)
            {
                Vector3 nextNodePos = GetComponent<PathHolderScript>().GetNextPos();

                Vector3 diff = nextNodePos - gameObject.transform.position;

                float dist = diff.magnitude;

                float minDist = 0.5f;

                if (dist < minDist)
                {
                    GetComponent<PathHolderScript>().KnockOutPathNode();
                    nextNodePos = GetComponent<PathHolderScript>().GetNextPos();
                }

                Vector2 diffToObj = closestObj.transform.position - gameObject.transform.position;
                float distToObj = diffToObj.magnitude;

                float minObjDist = 1.0f;

                if(distToObj < minObjDist)
                {
                    inObj = true;
                    SetMoveBool(false);

                    if (closestObj.tag == "Chest")
                    {
                        SetMoveBool(false);

                        canMove = false;

                        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "Where is that spell book?";
                        closestObj.GetComponent<ChestScript>().SetOccupation(true);

                        StartCoroutine(WaitForItem());
                    }
                    else if (closestObj.tag == "Booth")
                    {
                        SetMoveBool(false);

                        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "Hello! Somebody! Come Help!";
                        closestObj.GetComponent<PhoneBoothScript>().SetOccupation(true);

                        StartCoroutine(WaitForCall());
                    }
                }
                else
                {
                    if (canMove)
                    {
                        SetMovePoint(nextNodePos);
                    }
                }
            }
            else
            {
                GetComponent<RunToObjectScript>().RandomizeObjVal();
                closestObj = GetComponent<RunToObjectScript>().closeObj;
                Vector2 closeObjVec2 = closestObj.transform.position;
                Vector2 charObjVec2 = gameObject.transform.position;

                if (closestObj.name == "PhoneBooth")
                    gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "I gotta go call someone";
                else if (closestObj.name == "Chest")
                    gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "I gotta go grab a weapon";

                GetComponent<PathHolderScript>().GeneratePath(charObjVec2, closeObjVec2);
            }
        }
    }

    void CheckForPause()
    {
        if(GameMan!=null)
        {
            if (GameMan.GetComponent<GameManagerScript>().gamePaused)
            {
                canMove = false;
                SetMoveBool(false);
            }
            else
            {
                canMove = true;
                SetMoveBool(true);
            }
        }
        else
        {
            canMove = true;
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

    public void SetMovePoint(Vector2 newPoint)
    {
        GetComponent<MoveScript>().SetMoveVec2(newPoint);
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

    public IEnumerator WaitForCall()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        if (!GetComponent<HumanScript>().beingAttacked)
        {
            if (closestObj != null)
            {
                Vector2 knownLoc = GetComponent<ShootScript>().threatsKnownLoc;

                // call the police to (the last known loc of the threat)
                if (GetComponent<ShootScript>().threat != null)
                {
                    closestObj.GetComponent<PhoneBoothScript>().CallThePoPo(knownLoc);
                    GetComponent<ShootScript>().threat = null;
                }
            }

            inObj = false;

            SetPanic(false);
            SetMoveBool(true);

            SetTarget(null);

            GetComponent<WanderScript>().RandomizePoint();
        }

    }

    public IEnumerator WaitForItem()
    {
        yield return new WaitForSeconds(3.0f);

        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        inObj = false;

        SetGun(true);
        SetPanic(false);
        SetMoveBool(true);

        SetTarget(null);
        SetMovePoint(GetComponent<ShootScript>().threatsKnownLoc);

        GetComponent<PathHolderScript>().GeneratePath(gameObject.transform.position, GetComponent<ShootScript>().threatsKnownLoc);

    }

}
