using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    float shootingDist = 7.0f;
    float turnSpeed;

    float time = 0.0f;
    float fireRate = 1.0f;

    bool ableToShoot = true;
    bool targetSighted;
    bool hasGun = false;

    GameObject bullet;
    public GameObject target;

    public GameObject threat;
    public Vector2 threatsKnownLoc;

    // Use this for initialization
    void Start ()
    {
        bullet = Resources.Load("Prefabs/Bullet") as GameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckConditions();
    }

    void CheckConditions()
    {
        target = GetComponent<MoveScript>().target;
        turnSpeed = GetComponent<MoveScript>().turnSpeed;

        if (gameObject.tag == "Human")
        {
            targetSighted = GetComponent<HumanScript>().targetSighted;
            hasGun = GetComponent<HumanScript>().hasGun;
        }
        else if (gameObject.tag == "Policeman")
        {
            targetSighted = GetComponent<PolicemanScript>().targetSighted;
            hasGun = GetComponent<PolicemanScript>().hasGun;
        }

        if (targetSighted && hasGun)
        {
            if (gameObject.tag == "Human")
            {
                GetComponent<HumanScript>().SetMoveBool(false);
            }
            else if (gameObject.tag == "Policeman")
            {
                GetComponent<PolicemanScript>().SetMoveBool(false);
            }

            Aim();
        }
        else
        {
            if (gameObject.tag == "Human")
            {
                GetComponent<HumanScript>().SetMoveBool(true);
            }
            else if (gameObject.tag == "Policeman")
            {
                GetComponent<PolicemanScript>().SetMoveBool(true);
            }
        }
    }

    void Aim()
    {
        if (target != null)
        {
            // calculate angle between the character and the object
            float lookAngle;
            float lookAngleDeg;
            float prevAngle = transform.eulerAngles.z;

            float diff;
            float dist;

            float xDiff = target.transform.position.x - transform.position.x;
            float yDiff = target.transform.position.y - transform.position.y;

            lookAngle = Mathf.Atan2(yDiff, xDiff);

            lookAngleDeg = (lookAngle * Mathf.Rad2Deg);

            diff = lookAngleDeg - prevAngle;

            if (diff > 180)
            {
                diff -= 360;
            }
            else if (diff < -180)
            {
                diff += 360;
            }

            if (diff > 5)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            }
            else if (diff < -5)
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            }
            else
            {
                if (ableToShoot)
                {
                    dist = Mathf.Sqrt((xDiff * xDiff) + (yDiff * yDiff));

                    if (dist < shootingDist)
                    {
                        Shoot();
                    }
                    else
                    {
                        if(gameObject.tag == "Policeman")
                        {
                            GetComponent<PolicemanScript>().SetTargetSighted(false);
                            GetComponent<PolicemanScript>().SetTargetLastLoc(target.transform.position);
                            GetComponent<PolicemanScript>().SetTarget(null);

                        }
                        else if (gameObject.tag == "Human")
                        {
                            GetComponent<HumanScript>().SetTargetSighted(false);
                            GetComponent<HumanScript>().SetTargetLastLoc(target.transform.position);
                            GetComponent<HumanScript>().SetTarget(null);

                        }
                    }
                }
                else
                {
                    TimeBetweenShots();
                }


            }
        }
        else
        {
            if (gameObject.tag == "Policeman")
            {
                GetComponent<PolicemanScript>().SetTargetSighted(false);
            }
            else if (gameObject.tag == "Human")
            {
                GetComponent<HumanScript>().SetTargetSighted(false);
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

        GameObject newBullet;

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = (lookAngle * Mathf.Rad2Deg) - 90;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        newX = gameObject.transform.position.x;
        newY = gameObject.transform.position.y;

        Debug.Log("spawning new bullet");

        newBullet = Instantiate(bullet) as GameObject;
        newBullet.GetComponent<BulletScript>().SetAngle(lookAngleDeg);
        newBullet.transform.position = new Vector2(newX, newY);

        ableToShoot = false;
    }
    
    void TimeBetweenShots()
    {
        if(time < fireRate)
        {
            time = time + Time.deltaTime;
        }
        else
        {
            time = 0;
            ableToShoot = true;
        }
    }

    public void SetThreatLocation(GameObject newThreat)
    {
        // this will be used for calling the police or going back to the loc when armed
        threat = newThreat;
        threatsKnownLoc = threat.transform.position;

        if (hasGun)
        {
            target = newThreat;
        }
    }
}
