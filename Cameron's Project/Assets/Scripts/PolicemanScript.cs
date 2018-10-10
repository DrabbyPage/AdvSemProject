using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : MonoBehaviour
{
    GameObject target;
    //GameObject GameMan;
    GameObject bullet;

    float turnSpeed = 7.0f;
    float shootingDist = 10.0f;

    bool ableToAttack;
    bool beingAttacked;
    bool targetSighted;

    // Use this for initialization
    void Start ()
    {
        //GameMan = GameObject.Find("GameManager");

        ableToAttack = true;
        beingAttacked = false;
        targetSighted = false;
    }

    // Update is called once per frame
    void Update ()
    {
        beingAttacked = GetComponent<BeingAttackedScript>().GetBeingAttacked();

        if(!beingAttacked)
        {
            if (targetSighted)
                Aim();
            else
                GetComponent<WanderingScript>().SetMoving(true);
        }
        else
        {
            ableToAttack = false;
            GetComponent<WanderingScript>().SetMoving(false);
            GetComponent<BeingAttackedScript>().BeingAttacked();
        }

	}

    void Aim()
    {
        if (target != null)
        {
            // calculate angle between the character and the object
            float lookAngle;
            float prevAngle = transform.eulerAngles.z;

            float xDiff = target.transform.position.x - transform.position.x;
            float yDiff = target.transform.position.y - transform.position.y;

            float dist = Mathf.Sqrt((xDiff * xDiff) + (yDiff * yDiff));

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            float lookAngleDeg = (lookAngle * Mathf.Rad2Deg);

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

            if (diff > 3)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diff < -3)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                if(dist < shootingDist)
                {
                    if (ableToAttack)
                    {
                        if(bullet == null)
                        {
                            Shoot();
                        }
                    }
                    else
                    {
                        StartCoroutine(TimeBetweenShots());
                    }
                }
                else
                {
                    // move towards the target
                    //MoveToTarget();
                }

            }
        }

    }

    void Shoot()
    {
        // calculate angle between the character and the object
        float lookAngle;

        float newX, newY;

        float xDiff = target.transform.position.x - transform.position.x;
        float yDiff = target.transform.position.y - transform.position.y;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = (lookAngle * Mathf.Rad2Deg)-90;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        newX = gameObject.transform.position.x;
        newY = gameObject.transform.position.y;

        bullet = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
        bullet.GetComponent<BulletScript>().SetAngle(lookAngleDeg);
        bullet.transform.position = new Vector2(newX, newY);

        ableToAttack = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Zombie")
        {
            if(target == null)
            {
                Debug.Log("new target for policeman");
                targetSighted = true;
                target = col.gameObject;
            }
        }
    }

    IEnumerator TimeBetweenShots()
    {
        yield return new WaitForSeconds(0.75f);
        ableToAttack = true;
    }

    public void BulletIsGone()
    {
        bullet = null;
    }
}
