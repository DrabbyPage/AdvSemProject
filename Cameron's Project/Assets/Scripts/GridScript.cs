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

            testPath = GetPath(testStart, testEnd);
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

    List<GameObject> GetPath(Vector3 start, Vector3 end)
    {
        List<GameObject> path = new List<GameObject>();

        List<NodeRecord> openList = new List<NodeRecord>();
        List<NodeRecord> closedList = new List<NodeRecord>();

        GameObject startNode = FindClosestNode(start);
        GameObject endNode = FindClosestNode(end);

        NodeRecord startRecord = new NodeRecord();
        NodeScript.Connection startConnection = new NodeScript.Connection();

        startRecord.node = startNode;
        startRecord.recordConnection = startConnection;
        startRecord.estiTotalCost = EstimateCost(startNode, endNode);

        openList.Add(startRecord);

        NodeRecord currentNode = new NodeRecord();

        Debug.Log(endNode.name);
        Debug.Log(startNode.name);

        return null;

        //iterate through processing each node
        while (openList.Count > 0)
        {
            //find the smallest element in the open list
            currentNode = openList[0];

            // if it is the goal node then terminate
            if (currentNode.node == endNode)
            {
                Debug.Log("there is no route");
                break;
            }
            // otherwise get its outgoing connections
            else
            {
                // connections= graph.getConnections(current)
                List<NodeScript.Connection> curNodeConnect = new List<NodeScript.Connection>();
                curNodeConnect = currentNode.node.GetComponent<NodeScript>().GetConnections();

                // loop through each connection in turn
                for (int i = 0; i < curNodeConnect.Count; i++)
                {
                    // get the cost estimate for the end node
                    GameObject newEndNode = curNodeConnect[i].toNode;
                    float endNodeCost = currentNode.costSoFar + 1; // usually i would have to get the amount in
                                                                   // connection but we didnt have a cost amount for
                                                                   // connections so it is just 1

                    NodeRecord endNodeRecord = new NodeRecord();
                    float endNodeHeuristic;

                    bool inClosedList = false;
                    bool inOpenList = false;

                    // if the node is closed we may have to skip or remove it from the close list
                    //for (NodeRecord nodeRecord = closedList[0]; nodeRecord.node != closedList[closedList.Count-1].node; nodeRecord++)
                    for (int j = 0; j < closedList.Count; j++)
                    {
                        NodeRecord newNodeRecord = closedList[j];

                        // here we find the record in the closed list corresponding to the end node
                        if (newNodeRecord.node == endNode)
                        {
                            inClosedList = true;
                            endNodeRecord.node = newNodeRecord.node;
                        }

                        // if we didnt find a shorter route, skip
                        if (endNodeRecord.costSoFar <= endNodeCost)
                        {
                            // continue;
                            continue;
                        }
                        // otherwise remove it from the closed list;
                        else
                        {
                            closedList.Remove(newNodeRecord);

                            // we can use the node's old cost values to calculate its heuristic without
                            // calling the possibly expensive heuristic function
                            endNodeHeuristic = endNodeRecord.estiTotalCost - endNodeRecord.costSoFar;
                        }


                    }
                    if (!inClosedList)
                    {
                        // skip if the node is open and we've not found a better route
                        //for (auto record = openList.begin(); record != openList.end(); record++)
                        for (int j = 0; j < openList.Count; j++)
                        {
                            NodeRecord newNodeRecord = openList[j];

                            if (newNodeRecord.node == endNode)
                            {
                                inOpenList = true;

                                // here we find the record in the open list corresponding to the endNode
                                endNodeRecord.node = newNodeRecord.node;

                                // if our route is no better, then skip
                                if (endNodeRecord.costSoFar <= endNodeCost)
                                {
                                    // continue
                                    continue;
                                }
                                else
                                {

                                    // we can use the node's old cost values to calculate its heuristic without
                                    // calling the possibly expensive heuristic function
                                    endNodeHeuristic = endNodeRecord.recordConnection.weight - currentNode.costSoFar;
                                }
                                break;
                            }

                        }
                    }
                    // otherwise we know we've got an unvisited node so make a record for it
                    if (!inClosedList && !inOpenList)
                    {
                        endNodeRecord = new NodeRecord();
                        endNodeRecord.node = newEndNode;

                        // we'll need to calculate the value using the function, since
                        // we dont have an existing record to use
                        endNodeHeuristic = EstimateCost(newEndNode, endNode);

                        // we're here if we need to update the node
                        // update the cost, estimate, and connection
                        endNodeRecord.costSoFar = endNodeCost;
                        endNodeRecord.recordConnection = curNodeConnect[i];
                        endNodeRecord.estiTotalCost = endNodeCost + endNodeHeuristic;

                    }

                    // and add it to the openList
                    if (!inClosedList)
                    {
                        bool inOpen = false;
                        //for (auto record = openList.begin(); record < openList.end(); record++)
                        for (int j = 0; j < openList.Count; j++)
                        {
                            NodeRecord record = openList[j];

                            if (record.node == endNodeRecord.node)
                            {
                                inOpen = true;
                                break;
                            }
                        }

                        if (!inOpen)
                        {
                            openList.Add(endNodeRecord);
                        }
                    }
                }
                // we've finished looking at the connections for the current node so add it to the closed 
                // list and remove it from the open list
                openList.RemoveAt(0);
                closedList.Add(currentNode);
            }
        }


        // we're here if we found a goal or if we have no more nodes to search find which one
        if (currentNode.node != endNode)
        {
            // we've run out of nodes without finding the goal, so theres no solution
            // return none
            Debug.Log("did not end on goal node");
            return null;
        }
        // else:
        else
        {
            // compile the list of connections in the path
            List<GameObject> a_Star_Path = new List<GameObject>();

            // work back along the path, accumulating connections
            while (currentNode.node != startNode)
            {
                // path+= current.connection
                // current = current.connection.getFromNode()
                // FYI Update the current's connection as well
                a_Star_Path.Add(currentNode.node);
                currentNode.node = currentNode.recordConnection.fromNode;

                //for (auto node = closedList.begin(); node < closedList.end(); node++)
                for (int i = 0; i < closedList.Count; i++)
                {
                    
                    if (closedList[i].node == currentNode.node)
                    {
                        currentNode.recordConnection = closedList[i].recordConnection;
                    }
                    
                }
            }

            // reverse the path, and return it
            List<GameObject> reversePath = new List<GameObject>();

            for (int i = 0; i < a_Star_Path.Count; i++)
            {
                GameObject newNode;
                int lastNodeIndex;

                lastNodeIndex = a_Star_Path.Count - (i + 1);

                newNode = a_Star_Path[lastNodeIndex];

                reversePath.Add(newNode);
            }

            path = reversePath;
            return path;
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
