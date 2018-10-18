using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public GameObject player;

    public bool toggleFollow;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        toggleFollow = false;
    }

    // Update is called once per frame
    void Update ()
    {
        CheckToggle();
        UpdatePosition();
	}

    void CheckToggle()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleFollow();
        }
    }

    void UpdatePosition()
    {
        if(player != null && toggleFollow)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
        }
    }

    public void UpdatePlayerStatus(GameObject newPlayer)
    {
        player = newPlayer;
    }

    public void ToggleFollow()
    {
        if(toggleFollow)
        {
            toggleFollow = false;
        }
        else
        {
            toggleFollow = true;
        }
    }

    public bool getFollow()
    {
        return toggleFollow;
    }
}
