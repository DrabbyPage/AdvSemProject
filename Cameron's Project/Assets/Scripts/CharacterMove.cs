using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public GameObject target;
    GameObject GameMan;
    GameObject crosshair;
    
    public Vector2 newPoint;

    float moveSpeed = 65.0f;
    float turnSpeed = 7.0f;

    float targetRadius = 0.8f;

    float lockOnRange = 1.0f;

    public bool ableToMove = false;

    // Use this for initialization
    void Start()
    {
        target = null;
        newPoint = new Vector2(transform.position.x, transform.position.y);
        GameMan = GameObject.Find("GameManager");
        crosshair = Instantiate(Resources.Load("Prefabs/crosshair")) as GameObject;
        crosshair.SetActive(false);// = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNewPoint();
        CheckToMove();
        LookAtPoint();
    }

    void CheckForNewPoint()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);

            if (target != null)
            {
                target = null;
            }

            CheckZombieDist(mouseWorld);

            crosshair.GetComponent<SpriteRenderer>().color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);

            ableToMove = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));

            CheckHumanDist(mouseWorld);

        }
    }

    void CheckToMove()
    {
        if (ableToMove)
        {
            MoveToPoint();
        }
    }

    void MoveToPoint()
    {
        crosshair.SetActive(true);

        if (target != null)
        {
            newPoint = target.transform.position;
            crosshair.transform.position = target.transform.position;
        }
        else
        {
            crosshair.transform.position = newPoint;
        }

        float distance = Mathf.Sqrt(Mathf.Pow(newPoint.x - transform.position.x, 2) + Mathf.Pow(newPoint.y - transform.position.y, 2));

        if (distance < targetRadius)
        {
            newPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            crosshair.SetActive(false);
            ableToMove = false;
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed);
        }

    }

    void LookAtPoint()
    {
        float lookAngle;
        float currAngle = transform.eulerAngles.z;

        float lookX;
        float lookY;

        if (target != null)
        {
            lookX = target.transform.position.x;
            lookY = target.transform.position.y;
        }
        else
        {
            lookX = newPoint.x;
            lookY = newPoint.y;
        }

        float xDiff = lookX - transform.position.x;
        float yDiff = lookY - transform.position.y;

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
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
        }
        else if (diff < -10)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
        }
    }

    void CheckHumanDist(Vector3 mouse)
    {
        GameObject newTarget = GameMan.GetComponent<GameManagerScript>().CloseToHuman(mouse, lockOnRange);

        if (newTarget != null)
        {
            target = newTarget;
            crosshair.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            target = null;
        }
    }

    void CheckZombieDist(Vector3 mouse)
    {
        GameObject newTarget = GameMan.GetComponent<GameManagerScript>().CloseToZombie(mouse, lockOnRange);

        if (newTarget != null)
        {
            GameObject newZombie;

            newZombie = Instantiate(Resources.Load("Prefabs/Zombie")) as GameObject;
            newZombie.transform.position = gameObject.transform.position;

            GameMan.GetComponent<GameManagerScript>().AddZombieToList(newZombie);

            gameObject.transform.position = newTarget.transform.position;

            GameMan.GetComponent<GameManagerScript>().DeleteZombieFromList(newTarget);
            Destroy(newTarget);

            crosshair.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
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
