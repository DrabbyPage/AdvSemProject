using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMove : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotateToDirection();
	}

    void RotateToDirection()
    {
        Vector2 wanderPoint = gameObject.transform.parent.GetComponent<MoveScript>().walkToPoint;
        Vector2 charPoint = gameObject.transform.position;
        Vector2 direction = wanderPoint - charPoint;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
