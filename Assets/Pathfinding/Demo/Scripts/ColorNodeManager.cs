using UnityEngine;

namespace Ignita.Pathfinding.Demo
{
    /// <summary>
    /// Manages the color change of the nodes in the demo
    /// There a is a lot to improve in the demo architecture. Maybe later, it's enough for its purpose.
    /// </summary>
    public class ColorNodeManager : MonoBehaviour
    {
        [SerializeField] Color unvisitableColor = Color.gray;
        [SerializeField] Color paintPathColor = Color.cyan;
        [SerializeField] Color paintWaterColor = Color.blue;
        [SerializeField] Material tileMaterial = default;

        private Color visitableColor = Color.white;

        private void Start()
        {
            //The visitable color should be the default color of the tiles
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
}