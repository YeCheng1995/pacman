using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : IHeapItem<Node>
{

    public bool wall;
    public int x;
    public int y;
    public Vector3 pos;
    public int hcost;
    public int gcost;
    public int fcost{get { return hcost + gcost; }}
    public Node parent;
    int heapIndex;

    public Node(int x, int y, bool wall, Vector3 pos)
    {
        this.x = x;
        this.y = y;
        this.wall = wall;
        this.pos = pos;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fcost.CompareTo(nodeToCompare.fcost);
        if (compare == 0)
        {
            compare = hcost.CompareTo(nodeToCompare.hcost);
        }
        return -compare;
    }
}


