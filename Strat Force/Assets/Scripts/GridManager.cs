using UnityEngine;
using System.Collections.Generic;
public class GridManager : MonoBehaviour
{
    public List<List<GameObject>> grid = new List<List<GameObject>>();
    public int width = 10;
    public int height = 10; 
    public float tileSize = 2;
    public GameObject tilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantiate List
        for (int i = 0; i < width; i++)
        {
            List<GameObject> newList = new List<GameObject>(); // Creating new list
            for (int j = 0; j < height; j++)
            {
                newList.Add(null); // Populating that list with nothing
            }
            grid.Add(newList); // Adding the nothing list to the grid
        }

        // Generate Grid
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 position = new Vector3(i * tileSize, 0, j * tileSize); // Calculating position
                GameObject currentTile = Instantiate(tilePrefab, position, Quaternion.identity); // Creating object
                grid[i][j] = currentTile; // Setting object to tile position
            }
        }
    }

    // Use helper functions to see if a tile is occupied in a grid
    public bool IsOccupied(int x, int y)
    {
        return grid[x][y].GetComponent<GridTile>().insideTile != null;
    }

    public void Demonstration()
    {
        // Imagine you are the player tyring to move to the right 2 spaces
        int currentX = 1;
        int currentY = 1;

        grid[currentX + 2][currentY] = grid[currentX][currentY]; // Moving object
        grid[currentX][currentY] = null; // removing object from previous tile
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
