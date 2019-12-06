using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    public Transform player, goal;
    Node playerNode, goalNode;
    public LayerMask wallMask;
    public Vector2 gridSize;
    public float nodeRadius;
    public int nodeNumX, nodeNumY;
    float nodeDiameter;
    public Node[,] grid;
    public List<Node> path;
    public bool onlyDisplayPathGizmos;
    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        nodeNumX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        nodeNumY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreatGrid();
        

    }

    public int MaxSize
    {
        get
        {
            return nodeNumX * nodeNumY;
        }
    }

    private void CreatGrid()
    {
        grid = new Node[nodeNumX, nodeNumY];
        Vector3 startPos = transform.position - new Vector3(gridSize.x / 2, gridSize.y / 2, 0);
        for (int x = 0;x<nodeNumX;x++)
            for (int y = 0; y < nodeNumY; y++)
            {
                Vector3 currentPos = startPos + new Vector3(x * nodeDiameter + nodeRadius, y * nodeDiameter + nodeRadius, 0);
                bool walkable = !Physics.CheckSphere(currentPos, nodeRadius, wallMask);//check any stuff in nodeRadius range withallMask layout
                grid[x, y] = new Node(x, y, walkable, currentPos);
            } 
        
    }

    // Update is called once per frame
    void Update()
    {



    }

    public Node GetNodeFromPosition(Vector3 pos)
    {
        float precentX = (pos.x + gridSize.x / 2) / gridSize.x;
        float precentY = (pos.y + gridSize.y / 2) / gridSize.y;

        precentX = Mathf.Clamp01(precentX);
        precentY = Mathf.Clamp01(precentY);
        int x = Mathf.RoundToInt (precentX * (nodeNumX - 1));
        int y = Mathf.RoundToInt(precentY * (nodeNumY - 1));
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1f));

        if (onlyDisplayPathGizmos)
        {
            if (path != null)
            {
                foreach (Node n in path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.pos, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
        else { 

        if (grid != null)
            foreach(Node n in grid)
            {
                if (n.wall)
                {
                Gizmos.color = Color.gray;
                if (GetNodeFromPosition(player.position) == n)
                    Gizmos.color = Color.red;
                if (GetNodeFromPosition(goal.position) == n)
                    Gizmos.color = Color.white;
                    if (path != null && path.Contains(n))
                        foreach (Node nodepath in path)
                            Gizmos.color = Color.blue;
                Gizmos.DrawCube(n.pos, new Vector3(nodeDiameter * 0.9f, nodeDiameter * 0.9f, 1));
                }
                }
            }
    }
}
