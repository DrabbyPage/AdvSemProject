using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingScript : MonoBehaviour {

    Vector2 newPoint;
    Vector2 mousePos;

    float turnSpeed = 7.0f;

	// Use this for initialization
	void Start () {
        newPoint = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        CheckForNewPoint();
        LookAtPoint();
	}

    void CheckForNewPoint()
    {
        if(Input.GetMouseButton(0))
        {
            newPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.y));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);

            //Debug.Log(newPoint);
        }
    }

    void LookAtPoint()
    {
        float lookAngle;
        float prevAngle = transform.eulerAngles.z;

        float xDiff = newPoint.x - transform.position.x;
        float yDiff = newPoint.y - transform.position.y;
        
        lookAngle = Mathf.Atan2(yDiff, xDiff);

        float lookAngleDeg = lookAngle * Mathf.Rad2Deg;

        //Debug.Log(prevAngle + " vs " + lookAngleDeg);

        if(lookAngleDeg < 0)
        {
            lookAngleDeg = lookAngleDeg + 360;
        }

        if (lookAngleDeg > prevAngle + 9)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + turnSpeed);
            GetComponent<CharacterMove>().SetMoveAbility(false);
        }
        else if (lookAngleDeg < prevAngle - 9)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - turnSpeed);
            GetComponent<CharacterMove>().SetMoveAbility(false);
        }
        else
        {
            GetComponent<CharacterMove>().SetMoveAbility(true);
        }
        
    }
}
