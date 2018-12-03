using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToObjectScript : MonoBehaviour
{
    bool canCheckCondition = true;

    GameObject closeObj;

    float objVal = -1.0f;
    int numberOfItems = 2;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void RunToObj()
    {
        float dist;

        if(closeObj != null)
        {
            GetComponent<MoveScript>().SetTarget(closeObj);// MoveToPoint();

            float objX = closeObj.transform.position.x;
            float objY = closeObj.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - objX, 2) + Mathf.Pow(transform.position.y - objY, 2));

            if (dist > 0.4f)
            {
                if (GetComponent<MoveScript>().canMove)
                {
                    if (closeObj.name == "PhoneBooth")
                        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "I gotta go call someone";
                    else if(closeObj.name == "Chest")
                        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "I gotta go grab a weapon";

                    GetComponent<MoveScript>().MoveToPoint();
                }
            }
            else
            {
                GetComponent<HumanScript>().SetMoveBool(false);

                if(objVal == 1)
                {
                    gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "Hello! Somebody! Come Help!";
                    closeObj.GetComponent<PhoneBoothScript>().SetOccupation(true);
                    StartCoroutine(WaitForCall());
                }
                else if(objVal == 0)
                {
                    gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "Where is that spell book?";
                    closeObj.GetComponent<ChestScript>().SetOccupation(true);
                    StartCoroutine(WaitForItem());
                }

            }
        }
        else
        {
            RandomizeObjVal();
        }
    }

    // will come if there is no object yet set for the character to run to
    void RandomizeObjVal()
    {
        if (canCheckCondition)
        {
            objVal = Random.Range(0, 100) % numberOfItems;

            if (objVal == 1)
            {
                closeObj = GameObject.Find("GameManager").GetComponent<GameManagerScript>().ClosestBooth(gameObject.transform.position);

                if(closeObj == null)
                {
                    objVal = 0;
                }
            }
            if (objVal == 0)
            {
                closeObj = GameObject.Find("GameManager").GetComponent<GameManagerScript>().ClosestChest(gameObject.transform.position);

                if (closeObj == null)
                {
                    objVal = 1;
                }
            }
        }

        canCheckCondition = false;
    }

    IEnumerator WaitForCall()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        if (!GetComponent<HumanScript>().beingAttacked)
        {
            if (closeObj != null)
            {
                Vector2 knownLoc = GetComponent<ShootScript>().threatsKnownLoc;

                // call the police to (the last known loc of the threat)
                if (GetComponent<ShootScript>().threat != null)
                {
                    closeObj.GetComponent<PhoneBoothScript>().CallThePoPo(knownLoc);
                    GetComponent<ShootScript>().threat = null;
                }
            }

            canCheckCondition = true;

            GetComponent<HumanScript>().SetPanic(false);
            GetComponent<HumanScript>().SetMoveBool(true);

            GetComponent<MoveScript>().SetTarget(null);
        }

    }

    IEnumerator WaitForItem()
    {
        yield return new WaitForSeconds(3.0f);

        gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "";

        canCheckCondition = true;

        GetComponent<HumanScript>().SetGun(true);
        GetComponent<HumanScript>().SetPanic(false);
        GetComponent<HumanScript>().SetMoveBool(true);
        
        GetComponent<MoveScript>().SetTarget(null);
        GetComponent<MoveScript>().SetMoveVec2(GetComponent<ShootScript>().threatsKnownLoc);

    }

}
