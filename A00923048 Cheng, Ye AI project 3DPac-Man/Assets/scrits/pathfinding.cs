using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class pathfinding : MonoBehaviour
{
    Grid mygrid;
    List<Node> path;
    Node startNode, goalNode;

    private void Awake()
    {
        mygrid = GetComponent<Grid>();
      
    }

    void FindPath(Vector3 statPos, Vector3 goalPos)
    {
    
        startNode = mygrid.GetNodeFromPosition(statPos);
        goalNode = mygrid.GetNodeFromPosition(goalPos);
      
            List <Node> openSet = new List<Node>();
            HashSet<Node> closeSet = new HashSet<Node>();
            openSet.Add(startNode);


            while (openSet.Count > 0)
            {

                Node currentNote = openSet[0];
                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].fcost < currentNote.fcost || openSet[i].fcost == currentNote.fcost &&
                        openSet[i].hcost < currentNote.hcost)
                    {
                        currentNote = openSet[i];
                    }

                }
                openSet.Remove(currentNote);
                closeSet.Add(currentNote);

                if (currentNote == goalNode)
                {
                    print("path was found!");
                    retrievePath(currentNote);
                   
                    return;
                }
                foreach (Node n in GetNeighbors(currentNote))
                {
                    if (!n.wall || closeSet.Contains(n))
                        continue;
                    int newGCost = currentNote.gcost + GetDistance(currentNote, n);
                    bool inOpenSet = openSet.Contains(n);
                    if (newGCost < n.gcost || !inOpenSet)
                    {
                        n.gcost = newGCost;
                        n.hcost = GetDistance(n, goalNode);
                        n.parent = currentNote;
                        if (!inOpenSet)
                        {
                            openSet.Add(n);
                        }
                    }

                
            }
        }
    }

    private void retrievePath(Node n)
    {
        List<Node> p = new List<Node>();
        while(n != startNode)
        {
            p.Add(n);
            n = n.parent;
        }
        path = p;
    }

    int GetDistance(Node n1,Node n2)
    {
        int distanceX = (int)Math.Abs(n1.x - n2.x);
        int distanceY = (int)Math.Abs(n1.y - n2.y);
        if (distanceX > distanceY)
        {
            return 14 * (distanceY) + 10 * (distanceX - distanceY);
        }
        else
            return 14 * (distanceX) + 10 * (distanceY - distanceX);

    }
    private List<Node> GetNeighbors(Node n)
    {
        List<Node> neighbors = new List<Node>();
        int xx = n.x, yy = n.y;
        for(int x = -1;x<=1;x++)
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                if(xx+x>=0&&xx+x<mygrid.nodeNumX&&yy+y>=0&&yy+y<mygrid.nodeNumY)
                {
                    neighbors.Add(mygrid.grid[xx + x, yy + y]);
                }
            }
        return neighbors;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(mygrid.player.position, mygrid.goal.position);
    }
}
