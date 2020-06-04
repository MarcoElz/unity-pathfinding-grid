using System.Collections;
using UnityEngine;
using System;

namespace Ignita.Pathfinding.Demo
{
    /// <summary>
    /// Manages the demo scene. It's use to test the pathfinding algorithms
    /// </summary>
    public class DemoManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] Algorithm pathfindingAlgorithm = default;
        [SerializeField] float paintTimeDelay = 0.2f;

        [Header("References")]
        [SerializeField] Transform startPoint = default;
        [SerializeField] Transform endPoint = default;

        public enum Algorithm { BFS = 0, Dijkstra = 1, GreedyBFS = 2, AStar = 3 }

        public static DemoManager Instance { get; private set; }

        public event Action<Algorithm> onAlgorithmChange;

        private INode[] lastPath;
        private GridGenerator gridGenerator;
        private Coroutine paintCoroutine;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            gridGenerator = FindObjectOfType<GridGenerator>();
            gridGenerator.GenerateGrid();

            startPoint.position = new Vector3(0f, 0.5f, 0f);
            endPoint.position = new Vector3(gridGenerator.GridSize.x - 1f, 0.5f, gridGenerator.GridSize.y - 1f);

            ResetNodes();
            CleanPath();
        }

        //If the Node can be editted by clicking
        public bool CanEditNode()
        {
            return !DraggableOnGrid.IsDraggingObject;
        }

        //Changes the algorithm enum
        public void ChangeAlgorithm(int n)
        {
            int totalAlgorithms = 4;
            int x = (int)pathfindingAlgorithm;
            x += n;
            if (x < 0) x = totalAlgorithms - 1;
            if (x > totalAlgorithms - 1) x = 0;

            pathfindingAlgorithm = (Algorithm)x;
            onAlgorithmChange(pathfindingAlgorithm);
        }

        //Starts the pathfinding algorithm
        public void StartPathfinding()
        {
            //You could create any pathfinder:
            //var myPathFinder = new BFS(); / new GreedyBFS(); / new Dijkstra(); / new AStar();
            //and calculate the path:
            //INode[] path = myPathFinder.CalculatePath(start,end);

            //Create pathfinder by the selected value on the Algorithm enum
            IPathFinder pathFinder = CreatePathfinder(pathfindingAlgorithm); 
           
            if(pathFinder == null)
            {
                Debug.Log("Unsupported algorithm: " + pathfindingAlgorithm.ToString());
                return;
            }

            //Check for start and end nodes
            NodeBehaviour start = null;
            NodeBehaviour end = null;

            RaycastHit hitInfo;

            if (Physics.Raycast(startPoint.position, Vector3.down, out hitInfo, Mathf.Infinity))
                start = hitInfo.collider.transform.parent.GetComponent<NodeBehaviour>();

            if (Physics.Raycast(endPoint.position, Vector3.down, out hitInfo, Mathf.Infinity))
                end = hitInfo.collider.transform.parent.GetComponent<NodeBehaviour>();

            //If everything is ready, calculates the path
            if (start != null && end != null)
            {
                CleanPath(); //Clean previous painted path
                lastPath = pathFinder.CalculatePath(start, end);

                if (lastPath != null && lastPath.Length > 1)
                {
                    if (paintCoroutine != null) StopCoroutine(paintCoroutine);
                    paintCoroutine = StartCoroutine(PaintPathRoutine(lastPath, paintTimeDelay)); //Paint the path
                }
                else
                    Debug.LogWarning("There is not any path connecting start and end nodes.");
            }
            else
            {
                Debug.LogWarning("Can not find a path. Start (green) and End (red) points should be above a Node.");
            }
        }

        //Clean the painted path by repainting all nodes to its color type
        public void CleanPath()
        {
            if (paintCoroutine != null) StopCoroutine(paintCoroutine);

            NodeBehaviour[] nodes = FindObjectsOfType<NodeBehaviour>();

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].GetComponentInChildren<ChangeNodeTypeOnClick>().PaintByType();
            }
        }

        //Restarts all the nodes to start state
        public void ResetNodes()
        {
            CleanPath();

            NodeBehaviour[] nodes = FindObjectsOfType<NodeBehaviour>();

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].GetComponentInChildren<ChangeNodeTypeOnClick>().ResetType();
            }
        }

        // Gets the pathfinder object by the chosen algorithm
        private IPathFinder CreatePathfinder(Algorithm algorithm)
        {
            switch(algorithm)
            {
                case Algorithm.BFS:
                    return new BFS();
                case Algorithm.Dijkstra:
                    return new Dijkstra();
                case Algorithm.GreedyBFS:
                    return new GreedyBFS();
                case Algorithm.AStar:
                    return new AStar();
            }

            return null;
        }


        //Paint path over time usgin a coroutine
        public void PaintPath(INode[] path, float paintSpeed)
        {
            StartCoroutine(PaintPathRoutine(path, paintSpeed));
        }
        
        private IEnumerator PaintPathRoutine(INode[] path, float paintSpeed)
        {
            ColorNodeManager colorManager = FindObjectOfType<ColorNodeManager>();

            WaitForSeconds waitPaint = new WaitForSeconds(paintSpeed);

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] is NodeBehaviour)
                    colorManager.PaintPathNode((NodeBehaviour)path[i]);
                yield return waitPaint;
            }
        }

    }
}