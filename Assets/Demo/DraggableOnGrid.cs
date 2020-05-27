using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableOnGrid : MonoBehaviour
{
    public static bool IsDraggingObject { get; private set; }

    private void OnMouseDown()
    {
        IsDraggingObject = true;
    }

    private void OnMouseDrag()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.transform.parent != null)
            {
                NodeBehaviour node = hitInfo.collider.transform.parent.GetComponent<NodeBehaviour>();
                if (node != null && node.IsVisitable)
                {
                    int x = Mathf.RoundToInt(node.transform.position.x);
                    int z = Mathf.RoundToInt(node.transform.position.z);
                    transform.position = new Vector3(x, transform.position.y, z);
                }
            }                
        }
    }

    private void OnMouseUp()
    {
        if (IsDraggingObject)
            IsDraggingObject = false;
    }
}
