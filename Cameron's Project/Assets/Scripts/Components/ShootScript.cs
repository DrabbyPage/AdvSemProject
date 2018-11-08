using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    float shootingDist = 3.0f;

    float time = 0.0f;
    float fireRate = 1.4f;

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

        if(gameObject.tag == "Policeman")
        {
            fireRate = 0.8f;
        }
        else if( gameObject.tag == "Human")
        {
            fireRate = 1.4f;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!GetComponent<BeingAttackedScript>().beingAttacked)
        {
            CheckConditions();
        }
    }

    void CheckConditions()
    {
        target = GetComponent<MoveScript>().target;

        if (target == null)
        {
            if (gameObject.tag == "Human")
            {
                GetComponent<HumanScript>().SetTarget(null);
                GetComponent<HumanScript>().SetTargetSighted(false);
                GetComponent<HumanScript>().SetMoveBool(true);
            }
            else if (gameObject.tag == "Policeman")
            {
                GetComponent<PolicemanScript>().SetTarget(null);
                GetComponent<PolicemanScript>().SetTargetSighted(false);
                GetComponent<PolicemanScript>().SetMoveBool(true);
            }
        }

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

            if (ableToShoot)
            {
                Shoot();
            }
            else
            {
                TimeBetweenShots();
            }
            
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
            gameObject.GetComponent<Animator>().SetBool("Attacking", false);
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

        if(target.transform.position.x > gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        gameObject.GetComponent<Animator>().SetBool("Attacking", true);

        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = (lookAngle * Mathf.Rad2Deg) - 90;

        if (lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        newX = gameObject.transform.position.x;
        newY = gameObject.transform.position.y;


        float dist = Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);

        if (dist < shootingDist)
        {
            GameObject SoundMan = GameObject.Find("SoundManager");
            Debug.Log("bullet is shot");

            newBullet = Instantiate(bullet) as GameObject;
            newBullet.GetComponent<BulletScript>().SetAngle(lookAngleDeg);
            newBullet.transform.position = new Vector2(newX, newY);
            SoundMan.GetComponent<SoundManagerScript>().PlayShootSound();
            ableToShoot = false;
        }
        else
        {
            targetSighted = false;
            GetComponent<MoveScript>().SetMoveVec2(target.transform.position);
            GetComponent<MoveScript>().SetTarget(null);
        }


        //gameObject.GetComponent<Animator>().SetBool("Attacking", false);
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
            gameObject.GetComponent<Animator>().SetBool("Attacking", false);
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
