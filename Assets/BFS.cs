﻿using System.Collections;
using System.Collections.Generic;

public class BFS 
{

    Queue<Node> queue;

    List<Node> path;

    Dictionary<Node, Node> exploredAndExploredFrom;

    Node exploringNode;

    public Node[] FindPath(Node start, Node end)
    {

        queue = new Queue<Node>();
        exploredAndExploredFrom = new Dictionary<Node, Node>();


        queue.Enqueue(start);
        exploredAndExploredFrom.Add(start, start);

        while (queue.Count > 0)
        {
            exploringNode = queue.Dequeue();
         
            if(exploringNode.Equals(end))
            {
                //Debug.Log("End");
                break;
            }

            for (int i = 0; i < exploringNode.Neighbors.Length; i++)
            {
                Node neighbor = exploringNode.Neighbors[i];
                if(!exploredAndExploredFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    exploredAndExploredFrom.Add(neighbor, exploringNode);
                }
            }
        }

        List<Node> path = new List<Node>();
        Node nextInPath = end;
        while(nextInPath != start)
        {
            path.Add(nextInPath);
            if (exploredAndExploredFrom.ContainsKey(nextInPath))
                nextInPath = exploredAndExploredFrom[nextInPath];
            else
                break;
        }

        return path.ToArray();
    }
}