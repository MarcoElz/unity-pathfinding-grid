using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] Neighbors { get; private set; }


    public void SetNeighbors(params Node[] neighbors)
    {
        this.Neighbors = neighbors;
    }
}
