using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public GameObject target;

    GameObject GameMan;

    public bool canMove = true;

    // Use this for initialization
    void Start()
    {
        target = null;
        GameMan = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPause();

        if(canMove)
        {
            GetComponent<MoveScript>().MoveToPoint();
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
    
    public void SetNewTarget(GameObject newObj)
    {
        target = newObj;
        GetComponent<MoveScript>().SetTarget(newObj);
    }

    public void SetMovePoint(Vector2 newPos)
    {
        GetComponent<MoveScript>().SetMoveVec2(newPos);
        //newPoint = newPos;
    }
}
