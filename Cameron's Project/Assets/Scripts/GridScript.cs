using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    [SerializeField]
    int rows;

    [SerializeField]
    int columns;

    List<GameObject> grid;

    // Use this for initialization
    void Start ()
    {
        grid = new List<GameObject>();
        MakeGrid();
    }

    // Update is called once per frame
    void Update ()
    {

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

}
