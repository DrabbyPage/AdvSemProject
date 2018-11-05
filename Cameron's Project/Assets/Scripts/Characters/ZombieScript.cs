using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    bool attacking = false;
    public bool canMove = true;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // might change to wander and go to roar
        if(!attacking)
        {
            GetComponent<MoveScript>().MoveToPoint();
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
