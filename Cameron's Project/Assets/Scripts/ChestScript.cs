using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool inUse;

    GameObject user;
    GameObject timerBox;

    Animator chestAnimator;

    float time;
    float totalTime = 3.0f;

	// Use this for initialization
	void Start ()
    {
        inUse = false;

        timerBox = gameObject.transform.GetChild(0).gameObject;

        chestAnimator = gameObject.GetComponent<Animator>();

        time = totalTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateAnim();
        if(inUse)
        {
            CountDownTimer();
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
            //user = null;
            inUse = false;
        }
        else
        {
            inUse = true;
            //CountDownTimer();
        }
    }

    void CountDownTimer()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
            timerBox.transform.localScale = new Vector3(time / 5, timerBox.transform.localScale.y, timerBox.transform.localScale.z);
        }
        else
        {
            inUse = false;
            user = null;
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
