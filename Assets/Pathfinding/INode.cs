using UnityEngine;

namespace Ignita.Pathfinding
{
    /// <summary>
    /// The node use for the pathfinding algorithm
    /// </summary>
    public interface INode
    {
        INode[] Neighbors { get; }
        Vector3 Position { get; }

        bool IsVisitable { get; }
        int Weight { get; }


        void SetNeighbors(params INode[] neighbors);
    }
}