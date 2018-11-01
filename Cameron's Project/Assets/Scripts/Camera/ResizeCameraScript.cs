using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCameraScript : MonoBehaviour
{
    float minCameraSize = 5.0f;
    float maxCameraSize = 11.0f;
    float cameraSizeChange = 0.5f;

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

        if(scroll > 0 && GetComponent<Camera>().orthographicSize > minCameraSize)
        {
            ShrinkView();
        }
        else if(scroll < 0 && GetComponent<Camera>().orthographicSize < maxCameraSize)
        {
            ExpandView();
        }
    }

    void ShrinkView()
    {
        GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - cameraSizeChange;
    }
    
    void ExpandView()
    {
        GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + cameraSizeChange;
    }
}
