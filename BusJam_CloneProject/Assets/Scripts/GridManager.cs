using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    public int width = 5; 
    public int height = 5; 
    public float cellSize = 1f; 
    public GridCell cellPrefab; 

    public GridCell[,] grid; 
    public float gap = 0.1f;

    private GridCreatar _gridCreator;
    public GridCreatar GridCreator => _gridCreator;

    void Awake()
    {

    _gridCreator = GetComponent<GridCreatar>();
            

    }



    private void Start()
    {
        _gridCreator.grid = new GridCell[_gridCreator.width, _gridCreator.height];
        for (int i = 0; i < _gridCreator.transform.childCount; i++)
        {
            var gridPart = _gridCreator.transform.GetChild(i).GetComponent<GridCell>();
            _gridCreator.grid[gridPart.coordinates.x, gridPart.coordinates.y] = gridPart;
        }

        grid = _gridCreator.grid;

    }


    
    public GridCell GetCell(Vector2Int coordinates)
    {
        if (grid == null)
        {
            
            return null;
        }
        if (IsWithinBounds(coordinates))
        {
            return grid[coordinates.x, coordinates.y];
        }
        else
        {
            
            return null;
        }
    }

    
    bool IsWithinBounds(Vector2Int coordinates)
    {
        return coordinates.x >= 0 && coordinates.x < width &&
               coordinates.y >= 0 && coordinates.y < height;
    }

   
    public void ColorCell(Vector2Int coordinates, Color color)
    {
        GridCell cell = GetCell(coordinates);
        if (cell != null)
        {
            Renderer renderer = cell.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }
}
