using UnityEngine;

namespace Ignita.Pathfinding.Demo
{
    public enum NodeDemoType { Empty = 0, Unpassable = 1, Water = 2 }

    /// <summary>
    /// A simple script to change the type of the node between "empty", "unpassable" and "water".
    /// Probably should make an scriptable object to manages the types, but hey it's just for a simple demo:)
    /// </summary>
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

        private void ChangeType()
        {
            int newType = (int)type + 1;

            if (newType > 2)
                newType = 0;

            type = (NodeDemoType)newType;
            PaintByType();

        }

        public void PaintByType()
        {
            switch (type)
            {
                case NodeDemoType.Empty:
                    node.ChangeAttributes(true, 1);
                    FindObjectOfType<ColorNodeManager>().PaintNodeType(node);
                    break;
                case NodeDemoType.Unpassable:
                    node.ChangeAttributes(false, int.MaxValue);
                    FindObjectOfType<ColorNodeManager>().PaintNodeType(node);
                    break;
                case NodeDemoType.Water:
                    node.ChangeAttributes(true, 5);
                    FindObjectOfType<ColorNodeManager>().PaintNodeWater(node);
                    break;
            }
        }



    }

}