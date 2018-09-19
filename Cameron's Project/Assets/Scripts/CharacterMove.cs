using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {


    Vector2 newPoint;
    Vector2 mousePos;

    float moveSpeed = 10.0f;
    //float maxSpeed = 5.0f;
    //float minSpeed = 0.0f;

    bool ableToMove = false;

    float attackTime;

    // Use this for initialization
    void Start()
    {
        newPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        CheckForNewPoint();
        MoveToPoint();
    }

    void CheckForNewPoint()
    {
        if (Input.GetMouseButton(0))
        {
            newPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.y));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);

        }
    }

    void MoveToPoint()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(newPoint.x - transform.position.x, 2) + Mathf.Pow(newPoint.y - transform.position.y, 2));

        //Debug.Log(distance);

        if(distance > 0.5f && ableToMove)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed);
            
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            ableToMove = false;
        }
    }

    public void SetMoveAbility(bool newMove)
    {
        ableToMove = newMove;
    }

}
