using Roy_T.AStar.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ignita.Pathfinding
{
    /// <summary>
    /// Greedy Breadth First Search pathfinding algorithm
    /// https://en.wikipedia.org/wiki/Best-first_search#Greedy_BFS
    /// </summary>
    public class GreedyBFS : IPathFinder
    {
        List<INode> path; //The path that is calculated
        MinHeap<PathfinderNode> frontier; //Priority Queue that stores the next nodes to explored (but with priority)
        Dictionary<PathfinderNode, PathfinderNode> exploration; //Exploration list as <Node, cameFromNode> to trace a path

        Dictionary<INode, PathfinderNode> nodes; //Get the PathfinderNode for each INode

        public GreedyBFS()
        {
            //Initialize data structures
            frontier = new MinHeap<PathfinderNode>();
            nodes = new Dictionary<INode, PathfinderNode>();
            exploration = new Dictionary<PathfinderNode, PathfinderNode>();

            path = new List<INode>();
        }

        /// <summary>
        /// Get the last path calculated by the pathfinder
        /// </summary>
        /// <returns>Returns the path as an array of INodes. If there is no path yet the array will be empty </returns>
        public INode[] GetPath() { return path.ToArray(); }

        /// <summary>
        /// Using Greedy BFS algorithm finds the path from start node to the end node.
        /// The "greedy" part uses Euclidian distance as the priority to explore
        /// </summary>
        /// <param name="start">The first node to start the search</param>
        /// <param name="end">The goal node that want to be found from start</param>
        /// <returns>Returns the path as an array of INodes. If there is not an available path, the array will be empty</returns>
        public INode[] CalculatePath(INode start, INode end)
        {
            PathfinderNode startNode = PathfinderNode.GetPathfinderNodeOrCreateNew(nodes, start);

            frontier.Insert(startNode);
            exploration.Add(startNode, startNode);

            while (frontier.Count > 0)
            {
                PathfinderNode exploringNode = frontier.Extract();

                if (exploringNode.Node.Equals(end))
                    break;

                for (int i = 0; i < exploringNode.Node.Neighbors.Length; i++)
                {
                    INode neighbor = exploringNode.Node.Neighbors[i];

                    if (neighbor.IsVisitable)
                    {
                        PathfinderNode neighborNode = PathfinderNode.GetPathfinderNodeOrCreateNew(nodes, neighbor);

                        if (!exploration.ContainsKey(neighborNode)) //If is the first time to be explored
                        {
                            neighborNode.HeuristicValue = Heuristic.SquaredEuclidianDistance(neighbor, end);
                            frontier.Insert(neighborNode);
                            exploration.Add(neighborNode, exploringNode); //Add: neighborNode explored from exploringNode
                        }                        
                    }
                }
            }


            //Construct path   
            if (nodes.ContainsKey(end)) //If the end node was added by one of its neighbors
            {
                //Track the path from end to start
                PathfinderNode nextNode = nodes[end]; //Starts from end node

                while (nextNode != startNode)
                {
                    path.Add(nextNode.Node);
                    if (exploration.ContainsKey(nextNode)) //If the node was visited from other node
                        nextNode = exploration[nextNode]; //that "came from" node is the next in the path
                    else  //if the node doesn't exist
                        break; //stop
                }
                path.Reverse();
            }

            return path.ToArray();
        }
    }
}