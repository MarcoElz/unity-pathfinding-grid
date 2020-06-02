# Pathfinding Algorithms (grid-based) for Unity
A collection of some Path Finding Algorithms based on grids for Unity3d Game Engine.

## Usage Example

#### Node
This NodeBehaviour example implements the INode interface but also inherits from MonoBehaviour. 
You must implement the INode interface in your Node class to be able to use it with the pathfinding algorithms.
Don't forget to set the neighbors of the node. It's essential for the algorithms.
```C#
using Ignita.Pathfinding;

public class NodeBehaviour : MonoBehaviour, INode
    {
        public INode[] Neighbors { get; private set; }
        public Vector3 Position { get { return transform.position; } }
        public bool IsVisitable { get; private set; }
        public int Weight { get; private set; }

        // ...

        public void SetNeighbors(params INode[] neighbors)
        {
            this.Neighbors = neighbors;
        }
    }
```

#### Pathfinder
The pathfinder algorithm to calculate the path.
You can use the IPathFinder classes: BFS, GreedyBFS, Dijkstra, AStar.
```C#
using Ignita.Pathfinding;

// ...

//Create path finder object
//You can use: BFS, GreedyBFS, Dijkstra, AStar.
IPathFinder pathFinder = new BFS();

//Start the algorithim
pathFinder.CalculatePath(start, end); //start and end must be type INode.

```
You can check the included demo scene for more details.

## TODO
- [x] Breadth First Search (BFS)
- [x] Greedy BFS
- [x] Dijkstra
- [x] A*

## Credits
Thanks to [Roy Triesscheijn (roy-t)](https://github.com/roy-t "Roy Triesscheijn(roy-t) profile") for his great **MinHeap Collection** from his [AStart repo (check it out!)](https://github.com/roy-t/AStar "Roy-T.AStar repo")

## License
This library is licensed under the MIT license, see the [LICENSE file](https://github.com/MarcoElz/unity-pathfinding-grid/blob/master/LICENSE) for more details.
