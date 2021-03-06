﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAttackedScript : MonoBehaviour
{
    GameObject GameMan;

    float timeToTurn = 3.0f;
    float dyingTime = 2.0f;
    float time = 0.0f;

    public bool beingAttacked = false;

	// Use this for initialization
	void Start ()
    {
        GameMan = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update ()
    {
        if (beingAttacked)
        {
            StartCoroutine(BeingAttacked());
        }
	}

    private void FixedUpdate()
    {
        if(beingAttacked)
        {
            TurnRed();
        }
    }

    public void ScreamForHelp()
    {
        //underDistress = true;
        GameObject SoundMan = GameObject.Find("SoundManager");

        if(SoundMan !=null)
        {
            Vector2 diff = GameObject.Find("Player").transform.position - gameObject.transform.position;
            float distance = diff.magnitude;

            if (distance < 5)
            {
                SoundMan.GetComponent<SoundManagerScript>().PlayCharDeathSound();
            }
        }
    }

    public IEnumerator BeingAttacked()
    {
        GetComponent<Rigidbody2D>().simulated = false;

        GetComponent<Animator>().SetBool("Dead", true);

        ScreamForHelp();

        yield return new WaitForSeconds(dyingTime);

        gameObject.tag = "Dead";

        //GetComponent<SpriteRenderer>().color = new Vector4(0.8f, 0.0f, 0.0f, 1.0f);

        StartCoroutine(StartTurningDead());
    }

    void TurnRed()
    {
        float redColor = GetComponent<SpriteRenderer>().color.r;
        float timeModifier = 1000f;

        if(time < 1.0)
        {
            time = (time + Time.deltaTime) / timeModifier;
        }

        if (redColor < 1.0f)
        {
            GetComponent<SpriteRenderer>().color = new Vector4( time, 0.0f, 0.0f, 1.0f);
        }
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

        GameMan.GetComponent<GameManagerScript>().DeleteHumanFromList(gameObject);
        GameMan.GetComponent<GameManagerScript>().AddZombieToList(newZombie);

        Destroy(gameObject);
    }

    public void SetBeingAttacked(bool newAttack)
    {
        beingAttacked = newAttack;
    }

    public bool GetBeingAttacked()
    {
        return beingAttacked;
    }
}
