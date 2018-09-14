using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    List<GameObject> humanList;
    List<GameObject> zombieList;

	// Use this for initialization
	void Start () {
        humanList = new List<GameObject>();
        zombieList = new List<GameObject>();

        AddHumansToList();
        AddZombieToList();

        Debug.Log("Human count: " + humanList.Count);
        Debug.Log("Zombie count: " + zombieList.Count);

    }

    // Update is called once per frame
    void Update () {
		
	}

    void AddHumansToList()
    {
        foreach(GameObject human in GameObject.FindGameObjectsWithTag("Human"))
        {
            humanList.Add(human);
        }
    }

    void AddZombieToList()
    {
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            zombieList.Add(zombie);
        }
    }

    public void ConvertHumanToZombie(GameObject human)
    {
        humanList.Remove(human);
        zombieList.Add(human);
    }
    
    public GameObject FindClosestHuman(GameObject zombie)
    {
        float dist;
        float closestDist = 1000;
        int closeHumIndex = 0;

        for(int i = 0; i<humanList.Count; i++)
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

        Debug.Log(humanList[closeHumIndex].gameObject.name);

        return humanList[closeHumIndex];
    }

    // add a remove all from list 
}
