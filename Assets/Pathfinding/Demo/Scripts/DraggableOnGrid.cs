using UnityEngine;

namespace Ignita.Pathfinding.Demo
{
    /// <summary>
    /// Makes the Game Object to be draggable on the grid by using the mouse button
    /// In the demo scene it's used to move the start and end nodes
    /// </summary>
    public class DraggableOnGrid : MonoBehaviour
    {
        public static bool IsDraggingObject { get; private set; }

        private void OnMouseDown()
        {
            IsDraggingObject = true;
        }

        //Use a raycast to check whether or not the mouse is pointing on a node before moving the object
        private void OnMouseDrag()
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.transform.parent != null)
                {
                    //Only move the object if it is on a node
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
}