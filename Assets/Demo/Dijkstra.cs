using Roy_T.AStar.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Create a new PathNode class to mantain all things related to the constructions

public class Dijkstra 
{

    MinHeap<Node> queue;
    List<Node> path;

    Dictionary<Node, Connection> exploration;

    Node exploringNode;

    public IEnumerator CalculatePath(Node start, Node end)
    {
        queue = new MinHeap<Node>();
        exploration = new Dictionary<Node, Connection>();

        queue.Insert(start);
        exploration.Add(start, new Connection(start,start,0));

        while (queue.Count > 0)
        {
            exploringNode = queue.Extract();

            if (exploringNode.Equals(end))
            {
                //Debug.Log("End");
                break;
            }

            for (int i = 0; i < exploringNode.Neighbors.Length; i++)
            {
                Node neighbor = exploringNode.Neighbors[i];

                int cost = exploration[exploringNode].cost + neighbor.Weight;

                if (neighbor.IsVisitable)
                {
                    if(!exploration.ContainsKey(neighbor))
                    {
                        queue.Insert(neighbor);
                        exploration.Add(neighbor, new Connection(exploringNode, neighbor, cost));
                        neighbor.cost = cost;
                    }
                    else
                    {
                        if (cost < exploration[neighbor].cost)
                        {
                            exploration[neighbor] = new Connection(exploringNode, neighbor, cost);
                            neighbor.cost = cost;
                        }
                    }
                }

                GameObject.FindObjectOfType<ColorNodeManager>().PaintPathNode(neighbor);
                yield return null;
            }
            //GameObject.FindObjectOfType<DemoManager>().ResetColors();
        }

        path = new List<Node>();
        Node nextInPath = end;
        while (nextInPath != start)
        {
            path.Add(nextInPath);
            if (exploration.ContainsKey(nextInPath))
                nextInPath = exploration[nextInPath].from;
            else
                break;
        }

        path.Reverse();

        //return path.ToArray();
        GameObject.FindObjectOfType<DemoManager>().ResetColors();
        GameObject.FindObjectOfType<DemoManager>().PaintPath(path.ToArray(), 0.25f);

    }

    public Node[] GetPath()
    {
        return path.ToArray();
    }
}


public struct Connection
{
    public Node from;
    public Node to;
    public int cost;

    public Connection(Node from, Node to, int cost)
    {
        this.from = from;
        this.to = to;
        this.cost = cost;
    }
}