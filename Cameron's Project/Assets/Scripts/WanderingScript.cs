using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingScript : MonoBehaviour
{
    bool reachedPoint;

    Vector2 wanderPoint;

    float maxSpeed;
    float turnSpeed;

    float slowRadius;
    float targetRadius;

    bool ableToMove;

	// Use this for initialization
	void Start ()
    {
        reachedPoint = false;

        wanderPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        maxSpeed = 10.0f;
        turnSpeed = 7.0f;

        slowRadius = 5.0f;
        targetRadius = 0.5f;

        ableToMove = true;

        RandomizeWanderPoint();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ableToMove)
            CheckForBehavior();
	}

    void CheckForBehavior()
    {
        if(reachedPoint)
        {
            RandomizeWanderPoint();
        }
        else
        {
            MoveToPoint();
        }
    }

    void RandomizeWanderPoint()
    {
        float randX = 0;
        float randY = 0;
        float objX = transform.position.x;
        float objY = transform.position.y;
        float wanderArea = 5.0f;

        reachedPoint = false;

        randX = Random.Range(-100, 100);
        randY = Random.Range(-100, 100);

        randX /= 100;
        randY /= 100;

        Vector2 newPoint = new Vector2(objX + randX, objY + randY);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = newPoint - playerPos;

        float rotation = Mathf.Atan2(direction.y, direction.x);

        wanderPoint = new Vector2(transform.position.x + (wanderArea * Mathf.Cos(rotation)), transform.position.y + (wanderArea * Mathf.Sin(rotation)));

        StartCoroutine(WaitBeforeMove());
    }

    void MoveToPoint()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(wanderPoint.x - transform.position.x, 2) + Mathf.Pow(wanderPoint.y - transform.position.y, 2));

        if (distance < targetRadius)
        {
            reachedPoint = true;
        }
        else
        {
            LookAtPoint();
            GetComponent<Rigidbody2D>().AddForce(transform.right * ((maxSpeed * distance) / slowRadius));
        }

    }

    void LookAtPoint()
    {
        float lookAngle;
        float currAngle = transform.eulerAngles.z;

        float xDiff = wanderPoint.x - transform.position.x;
        float yDiff = wanderPoint.y - transform.position.y;

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

    public void SetMoving(bool newMove)
    {
        ableToMove = newMove;
    }

    IEnumerator WaitBeforeMove()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;

        yield return new WaitForSeconds(0.7f);

        reachedPoint = false;
    }
}
