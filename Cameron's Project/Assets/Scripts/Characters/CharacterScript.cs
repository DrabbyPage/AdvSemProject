using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public GameObject target;

    public bool canMove = true;

    // Use this for initialization
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            GetComponent<MoveScript>().MoveToPoint();
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
