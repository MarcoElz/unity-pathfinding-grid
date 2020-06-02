using Roy_T.AStar.Collections;
using System.Collections.Generic;

namespace Ignita.Pathfinding
{
    /// <summary>
    /// A* search pathfinding algorithm
    /// https://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    public class AStar : IPathFinder
    {
        List<INode> path; //The path that is calculated
        MinHeap<PathfinderNode> frontier; //Priority Queue that stores the next nodes to explored (but with priority)
        Dictionary<PathfinderNode, PathfinderNode> exploration; //Exploration list as <Node, cameFromNode> to trace a path

        Dictionary<INode, PathfinderNode> nodes; //Get the PathfinderNode for each INode

        public AStar()
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
        /// Using A* algorithm to find the path from start node to the end node.
        /// It uses the weight of the nodes to find the best path, 
        /// and also the distance from each node to the goal.
        /// </summary>
        /// <param name="start">The first node to start the search</param>
        /// <param name="end">The goal node that want to be found from start</param>
        /// <returns>Returns the path as an array of INodes. If there is not an available path, the array will be empty</returns>
        public INode[] CalculatePath(INode start, INode end)
        {
            PathfinderNode startNode = GetPathfinderNode(start);

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
                        PathfinderNode neighborNode = GetPathfinderNode(neighbor);

                        float cost = exploringNode.Cost + neighbor.Weight;

                        if (!exploration.ContainsKey(neighborNode)) //If is the first time to be explored
                        {
                            neighborNode.Cost = cost;
                            neighborNode.HeuristicValue = EuclidianDistance(neighbor, end); //Calculate the distance once
                            exploration.Add(neighborNode, exploringNode); //Add: neighborNode explored from exploringNode
                            frontier.Insert(neighborNode);
                        }
                        else //Already explored
                        { //Compares if this path cost less
                            if (cost < exploration[neighborNode].Cost)
                            {
                                //Change the previous connection, so that neighbor now came from exploringNode
                                neighborNode.Cost = cost;
                                exploration[neighborNode] = exploringNode;
                            }
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

        private float EuclidianDistance(INode node, INode goal)
        {
            return (node.Position - goal.Position).magnitude;
        }

        private PathfinderNode GetPathfinderNode(INode node)
        {
            //Return the element if exists
            if (nodes.ContainsKey(node))
                return nodes[node];

            //Create the element
            PathfinderNode pathfinderNode = new PathfinderNode(node);
            nodes.Add(node, pathfinderNode);

            return pathfinderNode;
        }
    }
}