using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: RECONNECT ALL...
public class NodeBehaviour : MonoBehaviour, INode
{
    public int Weight { get; private set; }

    public Vector3 Position { get; private set; }

    public bool IsVisitable { get; private set; }

    public INode[] Neighbors { get; private set; }

    private void Awake()
    {
    }

    public void SetVisitable(bool visitable)
    {
        IsVisitable = visitable;
    }

    public void SetNeighbors(params INode[] neighbors)
    {
        this.Neighbors = neighbors;
    }

    public void SetWeight(int value)
    {
        this.Weight = value;
    }
}
