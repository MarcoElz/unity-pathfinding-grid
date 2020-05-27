using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeDemoType { Empty = 0, Unpassable = 1, Water = 2 }

public class ChangeNodeTypeOnClick : MonoBehaviour
{
    [SerializeField] NodeBehaviour node = default;

    private NodeDemoType type;

    private void Start()
    {
        type = NodeDemoType.Empty;
    }

    private void OnMouseDown()
    {
        if (!DemoManager.Instance.CanPaint())
            return;

        ChangeType();
    }

    private void OnMouseEnter()
    {
        if (!DemoManager.Instance.CanPaint())
            return;

        if (Input.GetMouseButton(0))
        {
            ChangeType();
        }
    }

    public void Recolor()
    {
        switch (type)
        {
            case NodeDemoType.Empty:
                node.SetVisitable(true);
                node.SetWeight(1);
                FindObjectOfType<ColorNodeManager>().PaintNodeType(node);
                break;
            case NodeDemoType.Unpassable:
                node.SetVisitable(false);
                node.SetWeight(int.MaxValue);
                FindObjectOfType<ColorNodeManager>().PaintNodeType(node);
                break;
            case NodeDemoType.Water:
                node.SetVisitable(true);
                node.SetWeight(5);
                FindObjectOfType<ColorNodeManager>().PaintNodeWater(node);
                break;
        }
    }

    private void ChangeType()
    {
        int newType = (int)type + 1;

        if (newType > 2)
            newType = 0;

        type = (NodeDemoType)newType;
        Recolor();
        
    }

}


