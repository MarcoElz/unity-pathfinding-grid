using UnityEngine;

namespace Ignita.Pathfinding.Demo
{

    /// <summary>
    /// A node implementations using the features of the Unity Engine
    /// </summary>
    public class NodeBehaviour : MonoBehaviour, INode
    {
        public INode[] Neighbors { get; private set; }
        public Vector3 Position { get { return transform.position; } }
        public bool IsVisitable { get; private set; }
        public int Weight { get; private set; }

        public void ChangeAttributes(bool visitable, int weight)
        {
            this.IsVisitable = visitable;
            this.Weight = weight;
        }

        public void SetNeighbors(params INode[] neighbors)
        {
            this.Neighbors = neighbors;
        }
    }
}