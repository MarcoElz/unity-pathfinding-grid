using System.Collections.Generic;
using UnityEngine;

namespace Ignita.Pathfinding.Demo
{
    /// <summary>
    /// Generates an squared grid of nodes to test the pathfinding algorithm
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] int gridSize = 10;
        [SerializeField] GameObject tilePrefab = default;

        public Vector2 GridSize { get { return grid != null ? new Vector2(grid.Length, grid[0].Length) : Vector2.zero; } }

        private INode[][] grid;

        public void GenerateGrid()
        {
            GenerateGrid(gridSize, gridSize);
        }

        private void GenerateGrid(int width, int height)
        {
            //Generate matrix
            grid = new INode[width][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new INode[height];
            }

            //Generate objects
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x, 0f, y);
                    GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                    grid[x][y] = tile.GetComponent<INode>();
                    tile.name = "Tile (" + x + ", " + y + ")";
                }
            }

            //Add tile neighbors
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    INode node = grid[x][y];
                    List<INode> neighbors = new List<INode>();

                    //Check 4 sides
                    if (y + 1 < grid[x].Length && grid[x][y + 1] != null) neighbors.Add(grid[x][y + 1]);
                    if (x + 1 < grid.Length && grid[x + 1][y] != null) neighbors.Add(grid[x + 1][y]);
                    if (y - 1 >= 0 && grid[x][y - 1] != null) neighbors.Add(grid[x][y - 1]);
                    if (x - 1 >= 0 && grid[x - 1][y] != null) neighbors.Add(grid[x - 1][y]);

                    node.SetNeighbors(neighbors.ToArray());
                }
            }

            //Move Camera
            int offsetheight = height + Mathf.RoundToInt(((float)height/10f * 3f));
            Camera.main.transform.position = new Vector3(width / 2f - 0.5f, 10f, offsetheight / 2f - 0.5f);
            Camera.main.orthographicSize = width > height ? width / 2f : offsetheight / 2f;
        }
    }
}
