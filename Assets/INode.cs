using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: CLEAN
public interface INode 
{

    int Weight { get; }
    Vector3 Position { get;  }

    bool IsVisitable { get; }
    INode[] Neighbors { get;  }

    void SetWeight(int value);

    //public Node(Vector3 position) 
    //    : this(position, true, 1)
    //{ 
    //}
    //public Node(Vector3 position, bool visitable) 
    //    : this(position, visitable, 1)
    //{
    //}
    //public Node(Vector3 position, bool visitable, int weight)
    //{
    //    this.Position = position;
    //    this.IsVisitable = visitable;
    //    this.Weight = weight;
    //    this.cost = int.MaxValue;
    //}

    void SetNeighbors(params INode[] neighbors);

    //public void SetNeighbors(params Node[] neighbors)
    //{
    //    this.Neighbors = neighbors;
    //}

    //public void SetVisitable(bool value)
    //{
    //    IsVisitable = value;
    //}

    //public int CompareTo(Node other)
    //{
    //    if (other == null) return 1;
    //    return this.cost.CompareTo(other.cost);
    //}
}
