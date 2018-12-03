using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public struct Connection
    {
        public GameObject toNode;
        public GameObject fromNode;
        public float weight;
    }

    Vector2 pos;

    #region Node Connections

    Connection rightConnect;
    Connection leftConnect;
    Connection upConnect;
    Connection downConnect;

    #endregion

    [SerializeField]
    public bool endPoint = false;

    [SerializeField]
    bool showConnections = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

        if (searchRight != null)
        {
            //rightConnect = searchRight;
            rightConnect = new Connection();
            rightConnect.fromNode = gameObject;
            rightConnect.toNode = searchRight;
            rightConnect.weight = 1;
        }
        if (searchLeft != null)
        {
            //leftConnect = searchLeft;
            leftConnect = new Connection();
            leftConnect.fromNode = gameObject;
            leftConnect.toNode = searchLeft;
            leftConnect.weight = 1;
        }
        if (searchUp != null)
        {
            //upConnect = searchUp;
            upConnect = new Connection();
            upConnect.fromNode = gameObject;
            upConnect.toNode = searchUp;
            upConnect.weight = 1;
        }
        if (searchDown != null)
        {
            //downConnect = searchDown;
            downConnect = new Connection();
            downConnect.fromNode = gameObject;
            downConnect.toNode = searchDown;
            downConnect.weight = 1;
        }

        CheckShowConnections();
    }

    void CheckShowConnections()
    {
        if (showConnections)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);

            if (rightConnect.toNode != null && gameObject.GetComponent<LineRenderer>().positionCount >= 1)
            {
                MakeConnection(rightConnect.toNode, 1);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 1)
            {
                MakeConnection(gameObject, 1);
            }

            if (leftConnect.toNode != null && gameObject.GetComponent<LineRenderer>().positionCount >= 2)
            {
                MakeConnection(leftConnect.toNode, 2);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 2)
            {
                MakeConnection(gameObject, 2);
            }

            if (upConnect.toNode != null && gameObject.GetComponent<LineRenderer>().positionCount >= 3)
            {
                MakeConnection(upConnect.toNode, 3);
            }
            else if (gameObject.GetComponent<LineRenderer>().positionCount >= 3)
            {
                MakeConnection(gameObject, 3);
            }

            if (downConnect.toNode != null && gameObject.GetComponent<LineRenderer>().positionCount >= 4)
            {
                MakeConnection(downConnect.toNode, 4);
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

    public List<Connection> GetConnections()
    {
        List<Connection> tempList = new List<Connection>();

        if(upConnect.toNode != null)
        {
            tempList.Add(upConnect);
        }

        if (downConnect.toNode != null)
        {
            tempList.Add(downConnect);
        }

        if (leftConnect.toNode != null)
        {
            tempList.Add(leftConnect);
        }

        if (rightConnect.toNode != null)
        {
            tempList.Add(rightConnect);
        }

        return tempList;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Environment")
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
