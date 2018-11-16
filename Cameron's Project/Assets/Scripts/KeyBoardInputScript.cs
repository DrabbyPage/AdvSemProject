using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardInputScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetKeyBoardInput();
	}

    void GetKeyBoardInput()
    {
        // make the image highlighted when pressed
        if (Input.GetKey(KeyCode.W) && gameObject.name == "KeyButtonW")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.A) && gameObject.name == "KeyButtonA")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.S) && gameObject.name == "KeyButtonS")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.D) && gameObject.name == "KeyButtonD")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetMouseButton(0) && gameObject.name == "MouseButtonLeft")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetMouseButton(1) && gameObject.name == "MouseButtonRight")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }

        // retrun teh color when there is no input
        if (Input.GetKeyUp(KeyCode.W) && gameObject.name == "KeyButtonW")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (Input.GetKeyUp(KeyCode.A) && gameObject.name == "KeyButtonA")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (Input.GetKeyUp(KeyCode.S) && gameObject.name == "KeyButtonS")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (Input.GetKeyUp(KeyCode.D) && gameObject.name == "KeyButtonD")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (Input.GetMouseButtonUp(0) && gameObject.name == "MouseButtonLeft")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (Input.GetMouseButtonUp(1) && gameObject.name == "MouseButtonRight")
        {
            gameObject.GetComponent<Image>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
