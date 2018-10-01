using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : MonoBehaviour
{

    GameObject target;

    float policeSpeed = 5.0f * 0.01f;
    float turnSpeed = 7.0f;

    Vector2 pointToWalk;

    bool ableToAttack;

    // Use this for initialization
    void Start ()
    {
        ableToAttack = true;
        pointToWalk = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void Aim()
    {
        GameObject GameMan = GameObject.Find("GameManager");

        if (target != null)
        {
            // calculate angle between the character and the object
            float lookAngle;
            float prevAngle = transform.eulerAngles.z;

            float xDiff = target.transform.position.x - transform.position.x;
            float yDiff = target.transform.position.y - transform.position.y;

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

            float diff = lookAngleDeg - prevAngle;

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

            if (diff > 9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diff < -9)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                if (ableToAttack)
                {
                    Shoot();
                }
            }
        }

    }

    void Shoot()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Zombie")
        {
            
        }
    }

    IEnumerator TimeBetweenShots()
    {
        yield return new WaitForSeconds(0.75f);
        ableToAttack = true;
    }
}
