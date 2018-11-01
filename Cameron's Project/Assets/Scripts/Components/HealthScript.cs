using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    int health;
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
            // kill character
            if (gameObject.tag == "Player")
            {
                //transfer to other zombie
                GameObject closestZombie = GameMan.GetComponent<GameManagerScript>().ClosestZombie(gameObject.transform.position);

                if (closestZombie != null && closestZombie.tag != "Player")
                {
                    closestZombie.GetComponent<ZombieScript>().ConvertToPlayer();
                    Destroy(gameObject);
                }
                else
                {
                    // game over
                    Destroy(gameObject);
                    GameMan.GetComponent<GameManagerScript>().GameOver();
                }
            }
            else
            {
                // kill the obj
                GameMan.GetComponent<GameManagerScript>().DeleteZombieFromList(gameObject);
                Destroy(gameObject);

            }
        }
    }
}
