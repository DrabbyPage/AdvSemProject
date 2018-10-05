using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAttackedScript : MonoBehaviour
{
    GameObject gameManObj;
    GameObject playerObj;
    GameObject playerManager;

    float timeToTurn = 3.0f;
    //bool underDistress = false;

	// Use this for initialization
	void Start ()
    {
        gameManObj = GameObject.Find("GameManager");
        playerManager = GameObject.Find("Main Camera");
        playerObj = playerManager.GetComponent<FollowPlayerScript>().player;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void ScreamForHelp()
    {
        //underDistress = true;
    }

    public IEnumerator BeingAttacked()
    {
        float dyingTime;

        ScreamForHelp();

        playerObj = playerManager.GetComponent<FollowPlayerScript>().player;

        dyingTime = playerObj.GetComponent<AttackScript>().attackTime;

        yield return new WaitForSeconds(dyingTime);

        gameObject.tag = "Dead";

        GetComponent<SpriteRenderer>().color = new Vector4(0.8f, 0.0f, 0.0f, 1.0f);

        StartCoroutine(StartTurningDead());
    }

    IEnumerator StartTurningDead()
    {
        yield return new WaitForSeconds(timeToTurn);

        float objX, objY, objZ;

        objX = gameObject.transform.position.x;
        objY = gameObject.transform.position.y;
        objZ = gameObject.transform.position.z;

        Quaternion objQuat = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, 1.0f);

        Vector3 objPos = new Vector3(objX, objY, objZ);

        GameObject newZombie = Instantiate(Resources.Load("Prefabs/Zombie"),objPos, objQuat) as GameObject;

        gameManObj.GetComponent<GameManagerScript>().DeleteHumanFromList(gameObject);
        gameManObj.GetComponent<GameManagerScript>().AddZombieToList(newZombie);

        Destroy(gameObject);
    }
}
