using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BusManager))]
public class BusManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
      
        DrawDefaultInspector();

       
        BusManager busManager = (BusManager)target;

        
        if (GUILayout.Button("Create Bus"))
        {
            busManager.CreateBus();
        }

       
        if (GUILayout.Button("Create BusStop"))
        {
            busManager.CreateBusStop();
        }
    }
}
