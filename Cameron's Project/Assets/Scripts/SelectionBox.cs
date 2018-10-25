using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour {

    Vector3 initialMousePos;
    Color regColor;

	// Use this for initialization
	void Start ()
    {
        regColor = GetComponent<SpriteRenderer>().color;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForMouseInput();
	}
    
    void CheckForMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));

            initialMousePos = mouseWorld;

            gameObject.GetComponent<SpriteRenderer>().color = new Color(regColor.r, regColor.g, regColor.b, regColor.a);

            gameObject.transform.position = new Vector3(mouseWorld.x + 0.5f, mouseWorld.y - 0.5f, gameObject.transform.position.z);

        }
        else if(Input.GetMouseButton(0))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(regColor.r, regColor.g, regColor.b, regColor.a);
            CreateBox();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            initialMousePos = new Vector3(0,0,0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(regColor.r, regColor.g, regColor.b, 0.0f);
        }
    }

    void CreateBox()
    {
        Vector2 distBetweenMousePos;
        Vector3 newMouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));

        distBetweenMousePos = newMouse - initialMousePos;

        float newScaleX = distBetweenMousePos.x;
        float newScaleY = distBetweenMousePos.y;

        float newPosX = initialMousePos.x + (distBetweenMousePos.x / 2);
        float newPosY = initialMousePos.y + (distBetweenMousePos.y / 2);

        gameObject.transform.position = new Vector3(newPosX, newPosY, gameObject.transform.position.z);
        gameObject.transform.localScale = new Vector3(newScaleX, newScaleY, gameObject.transform.localScale.z);
    }
}
