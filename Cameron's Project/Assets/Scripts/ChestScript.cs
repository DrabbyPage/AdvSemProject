using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool inUse;

    GameObject user;

    Animator chestAnimator;

	// Use this for initialization
	void Start ()
    {
        inUse = false;

        chestAnimator = gameObject.GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateAnim();

        if(user != null)
        {
            CheckUserDist();
        }
	}

    void CheckUserDist()
    {
        float dist;
        float userX = user.transform.position.x;
        float userY = user.transform.position.y;
        dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - userX, 2) + Mathf.Pow(transform.position.y - userY, 2));

        if (dist > 1.0f)
        {
            user = null;
            inUse = false;
        }
    }

    void UpdateAnim()
    {
        chestAnimator.SetBool("ChestInUse", inUse);
    }

    public void ChangeUse(bool newBool)
    {
        inUse = newBool;
    }

    public void SetUser(GameObject newUser)
    {
        user = newUser;
    }
}
