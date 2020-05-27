using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderNode : IComparable<PathfinderNode>
{
    public INode node;
    public int Cost { get; set; }

    public PathfinderNode(INode node)
    {
        this.node = node;
    }

    public int CompareTo(PathfinderNode other)
    {
        if (other == null) return 1;
        return this.Cost.CompareTo(other.Cost);
    }
}
