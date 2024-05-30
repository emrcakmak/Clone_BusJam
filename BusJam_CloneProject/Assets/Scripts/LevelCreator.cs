#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class LevelCreator : MonoBehaviour
{
    [SerializeField]private string levelName = "NewLevel";



    [ContextMenu("Create Level")]
    public void SaveCurrentLevel()
    {
        string path = "Assets/Scenes/" + levelName + ".unity";
        Scene currentScene = SceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(currentScene, path);
        Debug.Log("Seviye kaydedildi: " + path);
        AddSceneToBuildSettings(path);
    }



    private void AddSceneToBuildSettings(string scenePath)
    {
        
        EditorBuildSettingsScene[] originalScenes = EditorBuildSettings.scenes;

       
        EditorBuildSettingsScene[] newScenes = new EditorBuildSettingsScene[originalScenes.Length + 1];
        for (int i = 0; i < originalScenes.Length; i++)
        {
            newScenes[i] = originalScenes[i];
        }

        
        EditorBuildSettingsScene newScene = new EditorBuildSettingsScene(scenePath, true);
        newScenes[newScenes.Length - 1] = newScene;

        
        EditorBuildSettings.scenes = newScenes;

        Debug.Log("Seviye Build Settings'e eklendi: " + scenePath);
    }

}
#endif