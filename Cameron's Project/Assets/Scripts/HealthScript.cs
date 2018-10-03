using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    int health;

	// Use this for initialization
	void Start ()
    {
        health = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HurtCharacter()
    {
        health = health - 1;

        if(health <= 1)
        {
            // kill character
        }
    }
}
