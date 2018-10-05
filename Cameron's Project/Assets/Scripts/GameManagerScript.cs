using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    List<GameObject> humanList;
    List<GameObject> zombieList;
    List<GameObject> boothList;

	// Use this for initialization
	void Start ()
    {
        humanList = new List<GameObject>();
        zombieList = new List<GameObject>();
        boothList = new List<GameObject>();

        AddStartHumansToList();
        AddStartZombiesToList();
        AddBoothsToList();

        Debug.Log("Human count: " + humanList.Count);
        Debug.Log("Zombie count: " + zombieList.Count);
        Debug.Log("Booth count: " + boothList.Count);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    // finds the closest human ro the gameobject/ zombie
    public GameObject FindClosestHuman(GameObject zombie)
    {
        float dist;
        float closestDist = 1000;
        int closeHumIndex = -1;

        if (humanList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < humanList.Count; i++)
        {
            float distX = humanList[i].transform.position.x - zombie.transform.position.x;
            float distY = humanList[i].transform.position.y - zombie.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if(dist < closestDist)
            {
                closestDist = dist;
                closeHumIndex = i;
            }
        }

        if(closeHumIndex == -1)
        {
            return null;
        }
        else
        {
            return humanList[closeHumIndex];
        }
    }

    // checks to see if a human is within range
    public GameObject CloseToHuman(Vector3 point, float range)
    {

        float dist;
        int closeHumIndex = 0;

        if (humanList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < humanList.Count; i++)
        {
            float distX = humanList[i].transform.position.x - point.x;
            float distY = humanList[i].transform.position.y - point.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist <= range)
            {
                closeHumIndex = i;
                break;
            }
            else
            {
                if (i == humanList.Count - 1)
                {
                    //Debug.Log("no target");
                    return null;
                }
            }
        }

        //Debug.Log("target: " + humanList[closeHumIndex]);
        return humanList[closeHumIndex];
    }

    // checks for closest booth for humans to run to
    public GameObject ClosestBooth(Vector3 point)
    {
        float dist;
        float smallestDist = 1000f;
        int closeBoothIndex = -1;

        if (boothList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < boothList.Count; i++)
        {
            float distX = boothList[i].transform.position.x - point.x;
            float distY = boothList[i].transform.position.y - point.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist < smallestDist)// && boothList[i].GetComponent<PhoneBoothScript>().boothInUse == false)
            {
                if(boothList[i].gameObject.GetComponent<PhoneBoothScript>().boothInUse == false)
                {
                    smallestDist = dist;
                    closeBoothIndex = i;
                }

            }

        }

        //Debug.Log("target: " + humanList[closeHumIndex]);
        if (closeBoothIndex == -1)
        {
            return null;
        }
        else
        {
            return boothList[closeBoothIndex];
        }
    }

    // gives closest zombie from the list of zombies to the position given
    public GameObject ClosestZombie(Vector3 point)
    {
        float dist;
        float closestDist = 1000;
        int closeZomIndex = 0;

        if (zombieList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < zombieList.Count; i++)
        {
            float distX = zombieList[i].transform.position.x - point.x;
            float distY = zombieList[i].transform.position.y - point.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if(dist < closestDist)
            {
                closestDist = dist;
                closeZomIndex = i;
            }

        }

        return zombieList[closeZomIndex];
        
    }

    // add a remove all from list 
    public void AddStartHumansToList()
    {
        foreach (GameObject human in GameObject.FindGameObjectsWithTag("Human"))
        {
            humanList.Add(human);
        }
    }

    void AddStartZombiesToList()
    {
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            zombieList.Add(zombie);
        }
    }

    void AddBoothsToList ()
    {
        foreach (GameObject booth in GameObject.FindGameObjectsWithTag("Booth"))
        {
            boothList.Add(booth);
        }
    }

    public void AddHumanToList(GameObject newHum)
    {
        humanList.Add(newHum);
    }

    public void AddZombieToList(GameObject newZom)
    {
        zombieList.Add(newZom);
    }

    public void DeleteHumanFromList(GameObject human)
    {
        humanList.Remove(human);
    }

    public void DeleteZombieFromList(GameObject zombie)
    {
        zombieList.Remove(zombie);
    }

}
