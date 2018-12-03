using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    #region NodeRecord

    struct NodeRecord
    {
        public GameObject node;

        public NodeScript.Connection recordConnection;

        public float costSoFar;
        public float estiTotalCost;
    }
    
    #endregion

    [SerializeField]
    List<GameObject> testPath;

    [SerializeField]
    int rows;

    [SerializeField]
    int columns;

    List<GameObject> grid;

    bool canPressButton = true;

    // Use this for initialization
    void Start ()
    {
        grid = new List<GameObject>();
        MakeGrid();

        testPath = new List<GameObject>();
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canPressButton)
        {
            canPressButton = false;
            Vector2 testStart = new Vector2(-30.0f, -20.0f);
            Vector2 testEnd = new Vector2(0.0f, 0.0f);

            testPath = GetDijkstraPath(testStart, testEnd);
        }
    }

    void MakeGrid()
    {
        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                GameObject newNode = Instantiate(Resources.Load("Prefabs/NodeObj")) as GameObject;

                newNode.transform.parent = gameObject.transform;

                float nodePosX = gameObject.transform.position.x + i;
                float nodePosY = gameObject.transform.position.y + j;

                newNode.name = "Node_" + i + "_" + j;

                newNode.GetComponent<NodeScript>().SetGridPos(i,j);

                newNode.GetComponent<NodeScript>().SetPos(new Vector2(nodePosX, nodePosY));

                grid.Add(newNode);
            }
        }

        MakeGridConnections();
    }

    void MakeGridConnections()
    {
        for(int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<NodeScript>().MakeConnections();
        }
    }
    
    List<GameObject> GetDijkstraPath(Vector3 start, Vector3 end)
    {
        GameObject startNode = FindClosestNode(start);
        GameObject startEndNode = FindClosestNode(end);

        Debug.Log(startNode.name);
        Debug.Log(startEndNode.name);

        // initialize the record for the start nodes
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = startNode;
        startRecord.costSoFar = 0;

        //initialize the open and closed list
        List<NodeRecord> openList = new List<NodeRecord>();
        List<NodeRecord> closedList = new List<NodeRecord>();

        openList.Add(startRecord);

        NodeRecord currentNode = new NodeRecord();

        List<NodeScript.Connection> connections = new List<NodeScript.Connection>();

        // iterate through each process
        while(openList.Count > 0)
        {
            NodeRecord endNodeRecord = new NodeRecord();

            // find the smallest element in the open list
            currentNode = openList[0];

            // if we are at the goal node then terminate
            if(currentNode.node == startEndNode)
            {
                Debug.Log("at end of dijkstra");
                break;
            }
            // otherwise get the outgoing connections
            else
            {
                connections = openList[0].node.GetComponent<NodeScript>().GetConnections();

                // loop through each connection in turn
                for(int i = 0; i < connections.Count; i++)
                {
                    // get the cost estimate for teh end node
                    GameObject newEndNode = connections[i].toNode;
                    NodeScript.Connection newEndConnect = connections[i];
                    float endNodeCost = currentNode.costSoFar + connections[i].weight;

                    bool inClosedList = false;
                    bool inOpenList = false;

                    // skip if the node is closed
                    for(int j = 0; j < closedList.Count; j++)
                    {
                        if(closedList[j].node == newEndNode)
                        {
                            inClosedList = true;
                            break;
                        }
                    }
                    //else if open has the end node
                    if(!inClosedList)
                    {
                        for(int j = 0; j < openList.Count; j++)
                        {
                            if(openList[j].node == newEndNode)
                            {
                                // here we find the record in the oopen list corresponding to the endNode
                                endNodeRecord.node = openList[j].node;
                                inOpenList = true;


                                // if(endNodeRecord.cost <= endNodeCost)
                                if (endNodeRecord.costSoFar <= endNodeCost)
                                {
                                    // continue
                                    continue;
                                }
                                break;
                            }
                        }
                    }
                    if(!inOpenList && !inClosedList)
                    {
                        endNodeRecord.node = newEndNode;

                        // we are here of we need to update the node
                        // update the cost and connection
                        endNodeRecord.recordConnection = newEndConnect;
                    }

                    // add it into the open list
                    if(!inClosedList)
                    {
                        bool inOpen = false;
                        for(int j = 0; j < openList.Count; j++)
                        {
                            if(openList[j].node == endNodeRecord.node)
                            {
                                inOpen = true;
                            }
                        }
                        if(!inOpen)
                        {
                            openList.Add(endNodeRecord);
                        }
                    }
                }
            }

            // we have finished looking at the connections for the current node
            // so add it to the closed list and remove it from the open list
            openList.RemoveAt(0);
            closedList.Add(currentNode);
        }

        // we are here if we have either found the goal or if we 
        // have no more nodes to search, find which:
        if(currentNode.node != startEndNode)
        {
            //we have run out of nodes without findign the goal so 
            // there is no solution
            Debug.Log("there is no solution");
            canPressButton = true;
            return null;
        }
        else
        {
            List<GameObject> dijkstraPath = new List<GameObject>();

            //work back along the path, accumulating connections
            while(currentNode.node != startNode)
            {
                dijkstraPath.Add(currentNode.node);
                currentNode.node = currentNode.recordConnection.fromNode;

                for(int i = 0; i < closedList.Count; i++)
                {
                    if(closedList[i].node == currentNode.node)
                    {
                        currentNode.recordConnection = closedList[i].recordConnection;
                    }
                }
            }

            // the path is backwards so we must reverse the path
            List<GameObject> reversePath = new List<GameObject>();

            for(int i = 0; i < dijkstraPath.Count; i++)
            {
                GameObject newNode;
                int lastNodeIndex;

                lastNodeIndex = dijkstraPath.Count - (i + 1);

                newNode = dijkstraPath[lastNodeIndex];

                reversePath.Add(newNode);
            }

            Debug.Log("finished making the path");

            canPressButton = true;

            return reversePath;
        }
    }

    GameObject FindClosestNode(Vector2 point)
    {
        GameObject foundPoint = null;// = GameObject.Find("Node_" + point.x + "_" + point.y);

        float minDist = 1000;

        for(int i = 0; i < grid.Count; i++)
        {
            Vector2 diff = new Vector3(point.x, point.y, 0) - grid[i].transform.position;

            float dist = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2));

            if(dist < minDist)
            {
                minDist = dist;
                foundPoint = grid[i];
            }
        }

        return foundPoint;

    }

    float EstimateCost(GameObject startNode, GameObject endNode)
    {
        //Debug.Log(startNode.name);
        Vector2 fromGridPos = startNode.transform.position;
        Vector2 toGridPos = endNode.transform.position;
        Vector2 dist = toGridPos - fromGridPos;

        return dist.magnitude;
    }
    
}
