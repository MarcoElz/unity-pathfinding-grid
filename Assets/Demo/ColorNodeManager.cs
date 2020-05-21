using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorNodeManager : MonoBehaviour
{
    [SerializeField] Color unvisitableColor = Color.gray;
    [SerializeField] Color paintPathColor = Color.cyan;
    [SerializeField] Material tileMaterial = default;

    private Color visitableColor = Color.white;

    private void Start()
    {
        visitableColor = tileMaterial.color;
    }

    public void PaintNodeType(Node node)
    {
        if (node)
            node.GetComponentInChildren<MeshRenderer>().material.color = node.IsVisitable ? visitableColor : unvisitableColor;
    }

    public void PaintPathNode(Node node)
    {
        if (node)
            node.GetComponentInChildren<MeshRenderer>().material.color = paintPathColor;
    }
}
