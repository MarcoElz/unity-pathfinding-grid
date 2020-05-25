using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorNodeManager : MonoBehaviour
{
    [SerializeField] Color unvisitableColor = Color.gray;
    [SerializeField] Color paintPathColor = Color.cyan;
    [SerializeField] Color paintWaterColor = Color.blue;
    [SerializeField] Material tileMaterial = default;

    private Color visitableColor = Color.white;

    private void Start()
    {
        visitableColor = tileMaterial.color;
    }

    public void PaintNodeType(NodeBehaviour node)
    {
        if (node)
            node.GetComponentInChildren<MeshRenderer>().material.color = node.IsVisitable ? visitableColor : unvisitableColor;
    }

    public void PaintNodeWater(NodeBehaviour node)
    {
        if (node)
            node.GetComponentInChildren<MeshRenderer>().material.color = paintWaterColor;
    }

    public void PaintPathNode(NodeBehaviour node)
    {
        if (node)
            node.GetComponentInChildren<MeshRenderer>().material.color = paintPathColor;
    }
}
