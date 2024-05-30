using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
public class LevelPlayButton : MonoBehaviour
{
    public Button playbutton;
    private int levelindex;
    void Start()
    {

        if (PlayerPrefs.HasKey("Level"))
        {
            levelindex = PlayerPrefs.GetInt("Level");

            Debug.Log(PlayerPrefs.GetInt("Level"));
            if (levelindex >= EditorBuildSettings.scenes.Length - 1)
            {
                levelindex = 1;
                PlayerPrefs.SetInt("Level", levelindex);
                Debug.Log("girdi");
            }
        }
        else
        {
            levelindex = 1;
        }

        



        playbutton.onClick.AddListener(PlayButtonClick);
    }

    
    void Update()
    {
        
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(levelindex);


    }
}
