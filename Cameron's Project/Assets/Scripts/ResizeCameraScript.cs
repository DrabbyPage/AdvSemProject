using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCameraScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForScroll();
	}

    void CheckForScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll > 0 && GetComponent<Camera>().orthographicSize > 3)
        {
            ShrinkView();
        }
        else if(scroll < 0 && GetComponent<Camera>().orthographicSize < 11)
        {
            ExpandView();
        }
    }

    void ShrinkView()
    {
        GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - 0.5f;
    }
    
    void ExpandView()
    {
        GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + 0.5f;
    }
}
