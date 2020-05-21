using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] int gridSize = 10;
    [SerializeField] GameObject tilePrefab = default;

    public Vector2 GridSize { get { return grid != null ? new Vector2(grid.Length, grid[0].Length) : Vector2.zero; } }
    

    private Node[][] grid;

    public void GenerateGrid()
    {
        GenerateGrid(gridSize, gridSize);
    }

    private void GenerateGrid(int width, int height)
    {
        //Generate matrix
        grid = new Node[width][];
        for(int i = 0; i < grid.Length; i++)
        {
            grid[i] = new Node[height];
        }

        //Generate objects
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, 0f, y);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                grid[x][y] = tile.GetComponent<Node>();
                tile.name = "Tile ("+x+", "+y+")";
            }
        }

        //Add tile neighbours
        for (int x = 0; x < grid.Length; x++)
        {
            for (int y = 0; y < grid[x].Length; y++)
            {
                Node node = grid[x][y];
                List<Node> neighbors = new List<Node>();

                //Check 4 sides
                if (x - 1 >= 0 && grid[x - 1][y] != null) neighbors.Add(grid[x - 1][y]);
                if (x + 1 < grid.Length && grid[x + 1][y] != null) neighbors.Add(grid[x + 1][y]);
                if (y - 1 >= 0 && grid[x][y - 1] != null) neighbors.Add(grid[x][y - 1]);
                if (y + 1 < grid[x].Length && grid[x][y + 1] != null) neighbors.Add(grid[x][y + 1]);

                node.SetNeighbors(neighbors.ToArray());
            }
        }

        //Move Camera
        Camera.main.transform.position = new Vector3(width / 2f - 0.5f, 10f, height / 2f - 0.5f);
        Camera.main.orthographicSize = width > height ? width / 2f : height / 2f;
    }

    
}
