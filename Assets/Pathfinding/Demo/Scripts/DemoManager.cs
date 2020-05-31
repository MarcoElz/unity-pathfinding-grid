using System.Collections;
using UnityEngine;

namespace Ignita.Pathfinding.Demo
{
    /// <summary>
    /// Manages the demo scene. It's use to test the pathfinding algorithms
    /// </summary>
    public class DemoManager : MonoBehaviour
    {
        [SerializeField] Transform startPoint = default;
        [SerializeField] Transform endPoint = default;
        [SerializeField] float paintTimeDelay = 0.2f;

        private INode[] lastPath;
        private GridGenerator gridGenerator;

        public static DemoManager Instance { get; private set; }

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
            ResetColors();
        }

        public void StartPathfinding()
        {
            IPathFinder pathFinder = new Dijkstra();

            NodeBehaviour start = null;
            NodeBehaviour end = null;

            RaycastHit hitInfo;

            if (Physics.Raycast(startPoint.position, Vector3.down, out hitInfo, Mathf.Infinity))
            {
                start = hitInfo.collider.transform.parent.GetComponent<NodeBehaviour>();
            }
            if (Physics.Raycast(endPoint.position, Vector3.down, out hitInfo, Mathf.Infinity))
            {
                end = hitInfo.collider.transform.parent.GetComponent<NodeBehaviour>();
            }

            if (start != null && end != null)
            {
                ResetColors();
                lastPath = pathFinder.CalculatePath(start, end);

                if (lastPath != null && lastPath.Length > 1)
                    StartCoroutine(PaintPathRoutine(lastPath, paintTimeDelay)); //Paint the path
                else
                    Debug.LogWarning("There is not any path connecting start and end nodes.");
            }
            else
            {
                Debug.LogWarning("Can not find a path. Start (green) and End (red) points should be above a Node.");
            }
        }

        public bool CanPaint()
        {
            return !DraggableOnGrid.IsDraggingObject;
        }

        public void ResetColors()
        {
            NodeBehaviour[] nodes = FindObjectsOfType<NodeBehaviour>();

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].GetComponentInChildren<ChangeNodeTypeOnClick>().PaintByType();
            }
        }

        private void ResetNodes()
        {
            NodeBehaviour[] nodes = FindObjectsOfType<NodeBehaviour>();

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].ChangeAttributes(true, 1);
            }
        }

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

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                ResetColors();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetNodes();
                ResetColors();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                StartPathfinding();
            }
        }

    }
}