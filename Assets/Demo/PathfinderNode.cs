using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderNode : IComparable<Node>
{
    private Node node;

    public int CompareTo(Node other)
    {
        if (other == null) return 1;
        return this.cost.CompareTo(other.cost);
    }
}
