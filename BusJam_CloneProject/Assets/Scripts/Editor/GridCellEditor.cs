using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridCell))]
public class GridCellEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();


        GridCell gridCell = (GridCell)target;


        if (GUILayout.Button("Create Character"))
        {
            gridCell.CreateACharacter();
        }

        if (GUILayout.Button("Remove Character"))
        {
            gridCell.RemoveTheCharacter();
        }


    }
}
