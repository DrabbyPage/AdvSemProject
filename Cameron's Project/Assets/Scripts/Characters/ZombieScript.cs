using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    GameObject GameMan;

    bool attacking = false;
    public bool canMove = true;

    // Use this for initialization
    void Start ()
    {
        GameMan = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForPause();

        // might change to wander and go to roar
        if(!attacking && canMove)
        {
            GetComponent<MoveScript>().MoveToPoint();
        }
	}

    void CheckForPause()
    {
        if (GameMan != null)
        {
            if (GameMan.GetComponent<GameManagerScript>().gamePaused)
            {
                gameObject.GetComponent<Animator>().SetBool("Walking", false);
                canMove = false;
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Walking", true);
                canMove = true;
            }
        }
        else
        {
            canMove = true;
        }

    }

    public void SetMoveBool(bool newMove)
    {
        canMove = newMove;
    }

    public void SetMovePoint(Vector2 newPos)
    {
        GetComponent<MoveScript>().SetMoveVec2(newPos);
    }

    public void ConvertToPlayer()
    {
        GameObject newPlayerObj;
        GameObject camera = GameObject.Find("Main Camera");
        GameObject GameMan = GameObject.Find("GameManager");

        newPlayerObj = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;

        newPlayerObj.name = "Player";
        newPlayerObj.tag = "Player";
        newPlayerObj.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        camera.GetComponent<FollowPlayerScript>().UpdatePlayerStatus(newPlayerObj);
        camera.GetComponent<OneClickSelectionScript>().SetPlayer(newPlayerObj);

        GameMan.GetComponent<GameManagerScript>().AddZombieToList(newPlayerObj);
        GameMan.GetComponent<GameManagerScript>().DeleteZombieFromList(gameObject);

        Destroy(gameObject);
    }

}
