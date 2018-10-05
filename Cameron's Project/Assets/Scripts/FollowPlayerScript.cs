using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update ()
    {
        UpdatePosition();
	}

    void UpdatePosition()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
    }

    public void UpdatePlayerStatus(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
