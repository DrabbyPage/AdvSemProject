using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    List<GameObject> humanList;
    List<GameObject> zombieList;

	// Use this for initialization
	void Start ()
    {
        humanList = new List<GameObject>();
        zombieList = new List<GameObject>();

        AddStartHumansToList();
        AddStartZombiesToList();

        Debug.Log("Human count: " + humanList.Count);
        Debug.Log("Zombie count: " + zombieList.Count);

    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void DeleteZombieFromList(GameObject zombie)
    {
        zombieList.Remove(zombie);
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
}
