using System;

namespace Ignita.Pathfinding
{
    /// <summary>
    /// A class use in some pathfinding algorithms to keep the cost of the path
    /// It's used in the Priority Queue comparing by it's cost
    /// </summary>
    public class PathfinderNode : IComparable<PathfinderNode>
    {
        /// <summary>
        /// The main node data
        /// </summary>
        public INode Node { get; private set; }

        /// <summary>
        /// The cost of the path up to this node
        /// </summary>
        public int Cost { get; set; }

        public PathfinderNode(INode node)
        {
            this.Node = node;
            Cost = int.MaxValue;
        }

        public int CompareTo(PathfinderNode other)
        {
            if (other == null) return 1;
            return this.Cost.CompareTo(other.Cost);
        }
    }
}