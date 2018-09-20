using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAttackedScript : MonoBehaviour {

    float timeToTurn = 7.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ScreamForHelp()
    {

    }

    public IEnumerator BeingAttacked()
    {
        ScreamForHelp();
        float dyingTime = GameObject.Find("Player").GetComponent<AttackScript>().attackTime;
        yield return new WaitForSeconds(dyingTime);
        gameObject.tag = "Dead";
        GetComponent<SpriteRenderer>().color = new Vector4(0.8f, 0.0f, 0.0f, 1.0f);
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().ConvertHumanToZombie(gameObject);
        StartCoroutine(StartTurningDead());
    }

    IEnumerator StartTurningDead()
    {
        yield return new WaitForSeconds(timeToTurn);

        GameObject newZombie;
        float objX, objY, objZ;

        objX = gameObject.transform.position.x;
        objY = gameObject.transform.position.y;
        objZ = gameObject.transform.position.z;

        Quaternion objQuat = new Quaternion(gameObject.transform.rotation.x,
                                            gameObject.transform.rotation.y,
                                            gameObject.transform.rotation.z, 1.0f);

        Vector3 objPos = new Vector3(objX, objY, objZ);

        newZombie = Instantiate(Resources.Load("Prefabs/Zombie"),objPos, objQuat) as GameObject;
        Destroy(gameObject);
    }
}
