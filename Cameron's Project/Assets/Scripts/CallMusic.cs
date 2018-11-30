using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallMusic : MonoBehaviour
{
    GameObject SoundMan;
    Scene currentScene;

	// Use this for initialization
	void Start ()
    {
        currentScene = SceneManager.GetActiveScene();

        SoundMan = GameObject.Find("SoundManager");

        if (currentScene.name == "Main_Menu")
        {
            CallMenuMusic();
        }
        else if (currentScene.name == "Level_Select")
        {
            CallLevelSelectMusic();
        }
        else
        {
            CallGameMusic();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(SoundMan == null)
        {
            SoundMan = GameObject.Find("SoundManager");
        }

        if(SoundMan != null)
        {
            if (SoundMan.GetComponent<SoundManagerScript>().MusicRecordPlayer.isPlaying == false)
            {
                if (currentScene.name == "Main_Menu")
                {
                    CallMenuMusic();
                }
                else if (currentScene.name == "Level_Select")
                {
                    CallLevelSelectMusic();
                }
                else
                {
                    CallGameMusic();
                }
            }

        }

    }

    void CallMenuMusic()
    {
        if (SoundMan == null)
        {
            SoundMan = GameObject.Find("SoundManager");
        }

        if (SoundMan != null)
        {
            SoundMan.GetComponent<SoundManagerScript>().PlayFantasyMusic();
        }
    }

    void CallLevelSelectMusic()
    {
        if (SoundMan == null)
        {
            SoundMan = GameObject.Find("SoundManager");
        }

        if (SoundMan != null)
        {
            SoundMan.GetComponent<SoundManagerScript>().PlayRaceMusic();
        }
    }

    void CallGameMusic()
    {
        if (SoundMan == null)
        {
            SoundMan = GameObject.Find("SoundManager");
        }

        if (SoundMan != null)
        {
            SoundMan.GetComponent<SoundManagerScript>().PlayArcadeMusic();
        }
    }
}
