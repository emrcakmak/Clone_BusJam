using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public GridManager gridManager;

    
    public List<GridCell> FindPath(Vector2Int start)
    {
        Debug.Log("Baþlangýç Koordinatlarý: " + start);

        
        GridCell startCell = gridManager.GetCell(start);

        if (startCell == null)
        {
            
            return null;
        }
        
       
        List<GridCell> openList = new List<GridCell>();
        HashSet<GridCell> closedList = new HashSet<GridCell>();

        openList.Add(startCell);

        while (openList.Count > 0)
        {
          
            GridCell currentCell = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentCell.fCost || (openList[i].fCost == currentCell.fCost && openList[i].hCost < currentCell.hCost))
                {
                    currentCell = openList[i];
                }
            }

            openList.Remove(currentCell);
            closedList.Add(currentCell);

            
            if (currentCell.coordinates.x == 0)
            {
                return RetracePath(startCell, currentCell);
            }

           
            foreach (GridCell neighbor in GetNeighbors(currentCell))
            {
                if (!neighbor.isWalkable || closedList.Contains(neighbor))
                {
                    continue;
                }

                int newCostToNeighbor = currentCell.gCost + GetDistance(currentCell, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = 0; 
                    neighbor.parent = currentCell;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        
        return null;
    }

    
    List<GridCell> RetracePath(GridCell startCell, GridCell endCell)
    {
        List<GridCell> path = new List<GridCell>();
        GridCell currentCell = endCell;

        while (currentCell != startCell)
        {
            path.Add(currentCell);
            currentCell = currentCell.parent;
        }

        path.Reverse();
        return path;
    }

    
    List<GridCell> GetNeighbors(GridCell cell)
    {
        List<GridCell> neighbors = new List<GridCell>();

        
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),   
            new Vector2Int(0, -1),  
            new Vector2Int(-1, 0),  
            new Vector2Int(1, 0)    
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighborCoordinates = cell.coordinates + dir;
            GridCell neighbor = gridManager.GetCell(neighborCoordinates);

            if (neighbor != null && neighbor.isWalkable)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

   
    int GetDistance(GridCell a, GridCell b)
    {
        int distX = Mathf.Abs(a.coordinates.x - b.coordinates.x);
        int distY = Mathf.Abs(a.coordinates.y - b.coordinates.y);

        return 10 * (distX + distY);
    }
}
