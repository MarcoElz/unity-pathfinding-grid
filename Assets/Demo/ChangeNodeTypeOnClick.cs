using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNodeTypeOnClick : MonoBehaviour
{
    [SerializeField] Node node = default;

    private void OnMouseDown()
    {
        if (!DemoManager.Instance.CanPaint())
            return;

        node.SetVisitable(!node.IsVisitable);
        FindObjectOfType<ColorNodeManager>().PaintNodeType(node);
    }

    private void OnMouseEnter()
    {
        if (!DemoManager.Instance.CanPaint())
            return;

        if (Input.GetMouseButton(0))
        {
            node.SetVisitable(!node.IsVisitable);
            FindObjectOfType<ColorNodeManager>().PaintNodeType(node);
        }
    }

}
