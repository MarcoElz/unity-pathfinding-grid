using System.Collections;
using System.Collections.Generic;

public class BFS : IPathFinder
{

    Queue<INode> queue;

    List<INode> path;

    Dictionary<INode, INode> exploredAndExploredFrom;

    INode exploringINode;

    public INode[] CalculatePath(INode start, INode end)
    {

        queue = new Queue<INode>();
        exploredAndExploredFrom = new Dictionary<INode, INode>();


        queue.Enqueue(start);
        exploredAndExploredFrom.Add(start, start);

        while (queue.Count > 0)
        {
            exploringINode = queue.Dequeue();
         
            if(exploringINode.Equals(end))
            {
                //Debug.Log("End");
                break;
            }

            for (int i = 0; i < exploringINode.Neighbors.Length; i++)
            {
                INode neighbor = exploringINode.Neighbors[i];
                if(neighbor.IsVisitable && !exploredAndExploredFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    exploredAndExploredFrom.Add(neighbor, exploringINode);
                }
            }
        }

        path = new List<INode>();
        INode nextInPath = end;
        while(nextInPath != start)
        {
            path.Add(nextInPath);
            if (exploredAndExploredFrom.ContainsKey(nextInPath))
                nextInPath = exploredAndExploredFrom[nextInPath];
            else
                break;
        }

        path.Reverse();

        return path.ToArray();
    }

    public INode[] GetPath()
    {
        return path.ToArray();
    }
}
