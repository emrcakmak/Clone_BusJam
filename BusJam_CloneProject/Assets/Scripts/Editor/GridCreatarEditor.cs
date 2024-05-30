using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridCreatar))]
public class GridCreatarEditor : Editor
{
    public override void OnInspectorGUI()
    {
       
        DrawDefaultInspector();

     
        GridCreatar gridCreatar = (GridCreatar)target;

     
        if (GUILayout.Button("Create Grid"))
        {
            gridCreatar.CreateGrid();
        }

       
    }
}
