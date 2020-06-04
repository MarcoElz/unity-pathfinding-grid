using System;
using System.Collections.Generic;

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
        public float Cost { get; set; }

        /// <summary>
        /// A value that represents the result of some heuristic operation, 
        /// for example, the euclidian distance from this node to the goal
        /// </summary>
        public float HeuristicValue { get; set; }

        public PathfinderNode(INode node)
        {
            this.Node = node;
        }

        public int CompareTo(PathfinderNode other)
        {
            if (other == null) return 1;
            return (Cost+HeuristicValue).CompareTo(other.Cost + other.HeuristicValue);
        }


        public static PathfinderNode GetPathfinderNodeOrCreateNew(Dictionary<INode, PathfinderNode> dictionary, INode node)
        {
            //Return the element if exists
            if (dictionary.ContainsKey(node))
                return dictionary[node];

            //Create the element
            PathfinderNode pathfinderNode = new PathfinderNode(node);
            dictionary.Add(node, pathfinderNode);

            return pathfinderNode;
        }
    }
}