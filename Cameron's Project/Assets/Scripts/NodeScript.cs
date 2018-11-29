using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour {

    Vector2 pos;

    #region Connections
    public GameObject rightConnect;
    public GameObject leftConnect;
    public GameObject upConnect;
    public GameObject downConnect;
    #endregion

    bool endPoint = false;

    [SerializeField]
    bool showConnections = true;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void SetPos(Vector2 newPos)
    {
        gameObject.transform.position += new Vector3(newPos.x, newPos.y, 0);
    }

    public void SetGridPos(int x, int y)
    {
        pos = new Vector2(x, y);
    }

    public void MakeConnections()
    {
        GameObject searchRight, searchLeft, searchUp, searchDown;

        searchRight = GameObject.Find("Node_" + (pos.x + 1) + "_" + pos.y);
        searchLeft = GameObject.Find("Node_" + (pos.x - 1) + "_" + pos.y);
        searchDown = GameObject.Find("Node_" + pos.x + "_" + (pos.y - 1));
        searchUp = GameObject.Find("Node_" + pos.x + "_" + (pos.y + 1));

        if(searchRight != null)
        {
            rightConnect = searchRight;
        }
        if (searchLeft != null)
        {
            leftConnect = searchLeft;
        }
        if (searchUp != null)
        {
            upConnect = searchUp;
        }
        if (searchDown != null)
        {
            downConnect = searchDown;
        }

        CheckShowConnections();
    }

    void CheckShowConnections()
    {
        if(showConnections)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);

            if(rightConnect != null && gameObject.GetComponent<LineRenderer>().positionCount >= 1)
            {
                MakeConnection(rightConnect, 1);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 1)
            {
                MakeConnection(gameObject, 1);
            }

            if (leftConnect != null && gameObject.GetComponent<LineRenderer>().positionCount >= 2)
            {
                MakeConnection(leftConnect, 2);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 2)
            {
                MakeConnection(gameObject, 2);
            }

            if (upConnect != null && gameObject.GetComponent<LineRenderer>().positionCount >= 3)
            {
                MakeConnection(upConnect, 3);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 3)
            {
                MakeConnection(gameObject, 3);
            }

            if (downConnect != null && gameObject.GetComponent<LineRenderer>().positionCount >= 4)
            {
                MakeConnection(downConnect, 4);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 4)
            {
                MakeConnection(gameObject, 4);
            }

        }
    }

    void MakeConnection(GameObject connection, int connectIndex)
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(connectIndex, connection.transform.position);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Environment")
        {
            endPoint = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Environment")
        {
            endPoint = true;
        }
    }
}
