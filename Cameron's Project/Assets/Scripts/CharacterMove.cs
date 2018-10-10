using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public GameObject target;
    GameObject GameMan;
    
    public Vector2 newPoint;

    float moveSpeed = 8.0f;
    float turnSpeed = 7.0f;

    float targetRadius = 0.8f;
    float slowRadius = 5.0f;

    float lockOnRange = 1.0f;

    public bool ableToMove = false;

    // Use this for initialization
    void Start()
    {
        target = null;
        newPoint = new Vector2(transform.position.x, transform.position.y);
        GameMan = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNewPoint();
        CheckToMove();
    }

    void CheckForNewPoint()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);

            CheckHumanDist(mouseWorld);
            ableToMove = true;
        }
    }

    void CheckToMove()
    {
        if (ableToMove)
        {
            MoveToPoint();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            ableToMove = false;
        }
    }

    void MoveToPoint()
    {
        if (target != null)
        {
            newPoint = target.transform.position;
        }

        float distance = Mathf.Sqrt(Mathf.Pow(newPoint.x - transform.position.x, 2) + Mathf.Pow(newPoint.y - transform.position.y, 2));

        if (distance < targetRadius)
        {
            newPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            ableToMove = false;
        }
        else
        {
            LookAtPoint();
            GetComponent<Rigidbody2D>().AddForce(transform.right * ((moveSpeed * distance) / slowRadius));
        }

    }

    void LookAtPoint()
    {
        float lookAngle;
        float currAngle = transform.eulerAngles.z;

        float xDiff = newPoint.x - transform.position.x;
        float yDiff = newPoint.y - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

        float diff = lookAngleDeg - currAngle;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        if (diff > 180)
        {
            diff -= 360;
        }
        else if (diff < -180)
        {
            diff += 360;
        }

        if (diff > 10)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
        }
        else if (diff < -10)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
        }
    }

    void CheckHumanDist(Vector3 mouse)
    {
        GameObject newTarget = GameMan.GetComponent<GameManagerScript>().CloseToHuman(mouse, lockOnRange);

        if (newTarget != null)
        {
            target = newTarget;
        }
        else
        {
            target = null;
        }
    }

    public void SetMoveAbility(bool newMove)
    {
        ableToMove = newMove;
    }

}
