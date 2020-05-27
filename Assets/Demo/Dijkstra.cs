using Roy_T.AStar.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Create a new PathINode class to mantain all things related to the constructions

public class Dijkstra : IPathFinder
{
    Dictionary<INode, PathfinderNode> nodes;

    MinHeap<PathfinderNode> queue;
    List<INode> path;

    Dictionary<PathfinderNode, PathfinderNode> exploration; //Node, fromNode

    PathfinderNode exploringNode;

    public INode[] CalculatePath(INode start, INode end)
    {
        nodes = new Dictionary<INode, PathfinderNode>();
        queue = new MinHeap<PathfinderNode>();
        exploration = new Dictionary<PathfinderNode, PathfinderNode>();

        PathfinderNode startNode = new PathfinderNode(start);
        startNode.Cost = int.MaxValue;
        nodes.Add(start, startNode);

        queue.Insert(startNode);
        exploration.Add(startNode, startNode);

        while (queue.Count > 0)
        {
            exploringNode = queue.Extract();

            if (exploringNode.Equals(end))
            {
                //Debug.Log("End");
                break;
            }

            for (int i = 0; i < exploringNode.node.Neighbors.Length; i++)
            {
                INode neighbor = exploringNode.node.Neighbors[i];  

                if (neighbor.IsVisitable)
                {
                    PathfinderNode neighborNode = default;// new PathfinderNode(neighbor);

                    if (nodes.ContainsKey(neighbor))
                    {
                        neighborNode = nodes[neighbor];
                    }
                    else
                    {
                        neighborNode = new PathfinderNode(neighbor);
                        neighborNode.Cost = int.MaxValue;
                        nodes.Add(neighbor, neighborNode);
                    }

                    int cost = exploringNode.Cost + neighbor.Weight;

                    if (!exploration.ContainsKey(neighborNode))
                    {
                        queue.Insert(neighborNode);
                        exploration.Add(neighborNode, exploringNode);
                        neighborNode.Cost = cost;
                    }
                    else
                    {
                        if (cost < exploration[neighborNode].Cost)
                        {
                            exploration[neighborNode] = exploringNode;
                            neighborNode.Cost = cost;
                        }
                    }
                }
                //GameObject.FindObjectOfType<ColorNodeManager>().PaintPathNode((NodeBehaviour)neighbor);
                //yield return null;
            }
            //yield return null;
        }

        path = new List<INode>();

        if(nodes.ContainsKey(end))
        {
            PathfinderNode nextInPathNode = nodes[end];

            while (nextInPathNode != startNode)
            {
                path.Add(nextInPathNode.node);
                if (exploration.ContainsKey(nextInPathNode))
                    nextInPathNode = exploration[nextInPathNode];
                else
                    break;

                //yield return null;
            }
            path.Reverse();
        }    

        return path.ToArray();
        //GameObject.FindObjectOfType<DemoManager>().ResetColors();
        //GameObject.FindObjectOfType<DemoManager>().PaintPath(path.ToArray(), 0.25f);

    }

    public INode[] GetPath()
    {
        return path.ToArray();
    }
}
