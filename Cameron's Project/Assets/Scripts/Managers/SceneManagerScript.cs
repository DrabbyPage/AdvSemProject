using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    string currentLevel;

	// Use this for initialization
	void Start ()
    {
        currentLevel = "Main_Menu";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadScene(string levelName)
    {
        currentLevel = levelName;
        SceneManager.LoadScene(levelName);
    }

    public string GetCurrentScene()
    {
        return currentLevel;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
