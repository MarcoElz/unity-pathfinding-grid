using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    [SerializeField] Transform startPoint = default;
    [SerializeField] Transform endPoint = default;

    [SerializeField] float paintTimeDelay = 0.2f;

    public Node[] lastPath;

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
    }

    public bool CanPaint()
    {
        return !DraggableOnGrid.IsDraggingObject;
    }

    private void ResetColors()
    {
        ColorNodeManager colorManager = FindObjectOfType<ColorNodeManager>();
        Node[] nodes = FindObjectsOfType<Node>();

        for(int i = 0; i < nodes.Length; i++)
        {
            colorManager.PaintNodeType(nodes[i]);
        }
    }

    private void ResetNodes()
    {
        Node[] nodes = FindObjectsOfType<Node>();

        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].SetVisitable(true);
        }
    }

    private IEnumerator PaintPath(Node[] path, float paintSpeed)
    {
        ColorNodeManager colorManager = FindObjectOfType<ColorNodeManager>();

        WaitForSeconds waitPaint = new WaitForSeconds(paintSpeed);

        for(int i = 0; i < path.Length; i++)
        {
            colorManager.PaintPathNode(path[i]);
            yield return waitPaint;
        }
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.C))
        {
            ResetColors();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetNodes();
            ResetColors();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            IPathFinder pathFinder = new BFS();
            
            Node start = null;
            Node end = null;

            RaycastHit hitInfo;

            if(Physics.Raycast(startPoint.position, Vector3.down, out hitInfo, Mathf.Infinity))
            {
                start = hitInfo.collider.transform.parent.GetComponent<Node>();             
            }
            if (Physics.Raycast(endPoint.position, Vector3.down, out hitInfo, Mathf.Infinity))
            {
                end = hitInfo.collider.transform.parent.GetComponent<Node>();
            }

            if(start != null && end != null)
            {
                ResetColors();

                lastPath = pathFinder.CalculatePath(start, end);

                StartCoroutine(PaintPath(lastPath, paintTimeDelay));
            }
            else
            {
                Debug.LogWarning("Can not find a path. Start (green) and End (red) points should be above a Node.");
            }
        }
    }
}
