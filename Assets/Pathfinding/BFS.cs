using System.Collections.Generic;

namespace Ignita.Pathfinding
{
    /// <summary>
    /// Breadth First Search pathfinding algorithm
    ///  https://en.wikipedia.org/wiki/Breadth-first_search
    /// </summary>
    public class BFS : IPathFinder
    {
        private List<INode> path; //The path that is calculated
        private Queue<INode> frontier; //The next nodes to explore
        private Dictionary<INode, INode> explored;//Exploration list as <Node, cameFromNode> to trace a path

        public BFS()
        {
            //Initialize data structures
            frontier = new Queue<INode>();
            explored = new Dictionary<INode, INode>();
            path = new List<INode>();
        }

        /// <summary>
        /// Get the last path calculated by the pathfinder
        /// </summary>
        /// <returns>Returns the path as an array of INodes. If there is no path yet the array will be empty </returns>
        public INode[] GetPath() { return path.ToArray(); }

        /// <summary>
        /// Using Breadth First Search algorithm to find the path from start node to the end node.
        /// </summary>
        /// <param name="start">The first node to start the search</param>
        /// <param name="end">The goal node that want to be found from start</param>
        /// <returns>Returns the path as an array of INodes. If there is not an available path, the array will be empty</returns>
        public INode[] CalculatePath(INode start, INode end)
        {
            frontier.Enqueue(start);
            explored.Add(start, start);

            while (frontier.Count > 0)
            {
                INode exploringINode = frontier.Dequeue();

                if (exploringINode.Equals(end))
                    break;

                for (int i = 0; i < exploringINode.Neighbors.Length; i++)
                {
                    INode neighbor = exploringINode.Neighbors[i];
                    if (neighbor.IsVisitable && !explored.ContainsKey(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                        explored.Add(neighbor, exploringINode);
                    }
                }
            }


            //Construct path   
            INode nextNode = end;
            while (nextNode != start)
            {
                path.Add(nextNode);
                if (explored.ContainsKey(nextNode)) //If the node was visited from other node
                    nextNode = explored[nextNode]; //that "came from" node is the next in the path
                else //if the node doesn't exist
                    break; //stop
            }

            path.Reverse();

            return path.ToArray();
        }


    }
}