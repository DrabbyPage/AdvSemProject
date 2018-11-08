using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    int health;

    float deathAnimTime = 3.0f;

    public bool isDying = false;

    GameObject GameMan;

	// Use this for initialization
	void Start ()
    {
        health = 1;
        GameMan = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HealCharacter(int healVal)
    {
        health = health + healVal;
    }

    public void HurtCharacter()
    {
        health = health - 1;

        if(health <= 1)
        {
            GameObject SoundMan = GameObject.Find("SoundManager");

            isDying = true;

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            gameObject.GetComponent<Collider2D>().enabled = false;

            GetComponent<AttackScript>().SetAttackAbility(false);


            if (SoundMan != false)
            {
                SoundMan.GetComponent<SoundManagerScript>().PlayerZombieDeathSound();
            }

            // kill character
            if (gameObject.tag == "Player")
            {
                GetComponent<CharacterScript>().SetMoveBool(false);

                //transfer to other zombie
                GameObject closestZombie = GameMan.GetComponent<GameManagerScript>().ClosestZombie(gameObject.transform.position);

                if (closestZombie != null && closestZombie.tag != "Player")
                {
                    closestZombie.GetComponent<ZombieScript>().ConvertToPlayer();

                    GetComponent<Animator>().SetBool("Death", true);

                    StartCoroutine(WaitForDeathAnim());
                }
                else
                {
                    // game over
                    GetComponent<Animator>().SetBool("Death", true);

                    GameMan.GetComponent<GameManagerScript>().GameOver();
                }
            }
            else
            {
                GetComponent<ZombieScript>().SetMoveBool(false);

                // kill the obj
                GameMan.GetComponent<GameManagerScript>().DeleteZombieFromList(gameObject);

                GetComponent<Animator>().SetBool("Death", true);

                GetComponent<AttackScript>().SetAttackAbility(false);

                StartCoroutine(WaitForDeathAnim());
            }
        }
    }

    IEnumerator WaitForDeathAnim()
    {
        yield return new WaitForSeconds(deathAnimTime);
        Destroy(gameObject);

    }
}
