using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GridCreatar : MonoBehaviour
{
    public int width = 5;
    public int height = 5; 
    public float cellSize = 1f; 
    public GridCell cellPrefab; 

    public GridCell[,] grid; 
    public float gap = 0.1f;


    [ContextMenu("Create Grid")]
   public void CreateGrid()
    {
        // Eski grid hücrelerini temizle
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;
            DestroyImmediate(child);
        }

        grid = new GridCell[width, height];

        float gap = 0.1f;
        // Her bir hücreyi oluþtur
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 cellPosition = new Vector3(x * (cellSize + gap), 0, y * (cellSize + gap));
                GridCell newCell = Instantiate(cellPrefab, cellPosition, Quaternion.identity); 
                newCell.coordinates = new Vector2Int(x, y); 
                newCell.transform.parent = transform; 
                grid[x, y] = newCell; 
            }
        }

    }
}
