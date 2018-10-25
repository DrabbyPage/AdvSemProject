using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    List<GameObject> humanList;
    List<GameObject> zombieList;
    List<GameObject> boothList;
    List<GameObject> chestList;

    GameObject sceneMan;

    public GameObject mainCanvas;

    public Text gameText;

    //bool loadedLevel = false;

    bool checkingForBooth = false;
    bool checkingForChest = false;

	// Use this for initialization
	void Start ()
    {
        sceneMan = GameObject.Find("SceneManager");

        if (gameText != null)
        {
            gameText.text = "";
        }

        humanList = new List<GameObject>();
        zombieList = new List<GameObject>();
        boothList = new List<GameObject>();
        chestList = new List<GameObject>();

        AddStartHumansToList();
        AddStartZombiesToList();
        AddBoothsToList();
        AddChestsToList();

        Debug.Log("Human count: " + humanList.Count);
        Debug.Log("Zombie count: " + zombieList.Count);
        Debug.Log("Booth count: " + boothList.Count);
        Debug.Log("Chest count: " + chestList.Count);
    }

    // Update is called once per frame
    void Update ()
    {
        CheckForLevelEnd();
	}
    
    void CheckForLevelEnd()
    {
        if(humanList.Count <= 0)
        {
            StartCoroutine(LevelComplete());
        }
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
        int closeHumIndex = -1;

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
        if (closeHumIndex < 0)
        {
            return null;
        }
        else
        {
            return humanList[closeHumIndex];
        }
    }

    // checks for closest booth for humans to run to
    public GameObject ClosestBooth(Vector3 point)
    {
        float dist;
        float smallestDist = 1000f;
        int closeBoothIndex = -1;

        if(!checkingForBooth)
        {
            checkingForBooth = true;
            if (boothList.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < boothList.Count; i++)
            {
                if (boothList[i].GetComponent<PhoneBoothScript>().boothInUse == false)
                {
                    float distX = boothList[i].transform.position.x - point.x;
                    float distY = boothList[i].transform.position.y - point.y;

                    dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

                    if (dist < smallestDist)
                    {
                        if (boothList[i].gameObject.GetComponent<PhoneBoothScript>().boothInUse == false)
                        {
                            smallestDist = dist;
                            closeBoothIndex = i;
                        }

                    }
                }

            }

            if (closeBoothIndex == -1)
            {
                checkingForBooth = false;
                return null;
            }
            else
            {
                checkingForBooth = false;
                return boothList[closeBoothIndex];
            }
        }
        else
        {
            return null;
        }
    }

    // checks for closest booth for humans to run to
    public GameObject ClosestChest(Vector3 point)
    {
        float dist;
        float smallestDist = 1000f;
        int closeChestIndex = -1;

        if(!checkingForChest)
        {
            checkingForChest = true;
            if (chestList.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < chestList.Count; i++)
            {
                if (chestList[i].gameObject.GetComponent<ChestScript>().inUse == false)
                {
                    float distX = chestList[i].transform.position.x - point.x;
                    float distY = chestList[i].transform.position.y - point.y;

                    dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

                    if (dist < smallestDist)
                    {
                        smallestDist = dist;
                        closeChestIndex = i;
                    }
                }

            }

            if (closeChestIndex == -1)
            {
                checkingForChest = false;
                return null;
            }
            else
            {
                checkingForChest = false;
                return chestList[closeChestIndex];
            }
        }
        else
        {
            return null;
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

    public GameObject CloseToZombie(Vector3 point, float range)
    {
        float dist;
        int zomIndex = -1;

        if (zombieList.Count == 0)
        {
            Debug.Log("there arent any zombies");
            return null;
        }

        for (int i = 0; i < zombieList.Count; i++)
        {
            float distX = zombieList[i].transform.position.x - point.x;
            float distY = zombieList[i].transform.position.y - point.y;

            dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist <= range)
            {
                zomIndex = i;
                break;
            }
        }

        //Debug.Log("target: " + humanList[closeHumIndex]);
        if(zomIndex < 0)
        {
            return null;
        }
        else
        {
            return zombieList[zomIndex];
        }
    }

    // gives closest policeman from the list of humans to the position given
    public GameObject ClosestPoliceman(Vector3 point)
    {
        float dist;
        float closestDist = 1000;
        int closePoliceIndex = -1;

        if (humanList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < humanList.Count; i++)
        {
            if (humanList[i].tag == "Policeman")
            {
                float distX = humanList[i].transform.position.x - point.x;
                float distY = humanList[i].transform.position.y - point.y;

                dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closePoliceIndex = i;
                }
            }

        }

        if (closePoliceIndex == -1)
        {
            return null;
        }
        else
        {
            return humanList[closePoliceIndex];
        }

    }

    // add a remove all from list 
    public void AddStartHumansToList()
    {
        foreach (GameObject human in GameObject.FindGameObjectsWithTag("Human"))
        {
            humanList.Add(human);
        }

        foreach (GameObject policeman in GameObject.FindGameObjectsWithTag("Policeman"))
        {
            humanList.Add(policeman);
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

    void AddChestsToList()
    {
        foreach (GameObject chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            chestList.Add(chest);
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

    public void GameOver()
    {
        if(gameText != null)
        {
            gameText.text = "You Lost";
        }

        StartCoroutine(GoBackToMenu());
    }

    IEnumerator LevelComplete()
    {
        if (gameText != null)
        {
            gameText.text = "Level Complete";
        }

        yield return new WaitForSeconds(3.0f);
        sceneMan.GetComponent<SceneManagerScript>().LoadScene("Level_Select");
    }

    IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(3.0f);
        sceneMan.GetComponent<SceneManagerScript>().LoadScene("Main_Menu");
    }
}
