using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void RandomizePoint()
    {
        float randX;
        float randY;
        float wanderArea = 5.0f;

        randX = Random.Range(-wanderArea, wanderArea);
        randY = Random.Range(-wanderArea, wanderArea);

        float newPointX = gameObject.transform.position.x + randX;
        float newPointY = gameObject.transform.position.y + randY;

        Vector2 wanderPoint = new Vector2(newPointX, newPointY);

        gameObject.GetComponent<MoveScript>().SetMoveVec2(wanderPoint); // use if you want to call a diff script

        // return wanderPoint; // used if you want the randomize point to return to the move
    }
}
