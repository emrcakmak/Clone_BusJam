using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();

       
        LevelCreator levelCreator = (LevelCreator)target;

     
        if (GUILayout.Button("Create Level"))
        {
            levelCreator.SaveCurrentLevel();
        }

    }
}
