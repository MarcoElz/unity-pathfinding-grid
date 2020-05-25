using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: CLEAN
public class Node : IComparable<Node>
{

    public int Weight { get; private set; }
    public Vector3 Position { get; private set; }
    public int cost;

    public bool IsVisitable { get; private set; }
    public Node[] Neighbors { get; private set; }

    public void SetWeight(int value) { Weight = value; }

    public Node(Vector3 position) 
        : this(position, true, 1)
    { 
    }
    public Node(Vector3 position, bool visitable) 
        : this(position, visitable, 1)
    {
    }
    public Node(Vector3 position, bool visitable, int weight)
    {
        this.Position = position;
        this.IsVisitable = visitable;
        this.Weight = weight;
        this.cost = int.MaxValue;
    }

    public void SetNeighbors(params Node[] neighbors)
    {
        this.Neighbors = neighbors;
    }

    public void SetVisitable(bool value)
    {
        IsVisitable = value;
    }

    public int CompareTo(Node other)
    {
        if (other == null) return 1;
        return this.cost.CompareTo(other.cost);
    }
}
