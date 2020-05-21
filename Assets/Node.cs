using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] bool isVisitable = true;

    public bool IsVisitable { get { return isVisitable; }  }
    public Node[] Neighbors { get; private set; }

    public void SetNeighbors(params Node[] neighbors)
    {
        this.Neighbors = neighbors;
    }

    public void SetVisitable(bool value)
    {
        isVisitable = value;   
    }
}
