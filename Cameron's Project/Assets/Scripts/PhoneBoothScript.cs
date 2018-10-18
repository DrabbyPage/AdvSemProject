using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBoothScript : MonoBehaviour
{
    public bool boothInUse;
    GameObject user;

	// Use this for initialization
	void Start ()
    {
        boothInUse = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Human")
        {
            float dist;
            float distX, distY;

            distX = col.gameObject.transform.position.x - gameObject.transform.position.x;
            distY = col.gameObject.transform.position.y - gameObject.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist > 1.0f)
            {
                boothInUse = false;
            }
            else
            {
                user = col.gameObject;
                boothInUse = true;
            }
        }
    }

    public void SetOccupation(bool newBool)
    {
        boothInUse = newBool;
    }
    
}
