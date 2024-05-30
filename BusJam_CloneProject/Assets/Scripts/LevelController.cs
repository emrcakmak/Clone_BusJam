using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelController : MonoBehaviour
{
    public GameObject GameWinPanel;
    public GameObject GameLosePanel;
    public TMP_Text currentLevel;
    private TimerManager TM_Manager;
    public int currentLeveIndex = 1;
    public Button play;
    public Button retry;
    public Button next;
    public Button goManinMenu;
    private CharacterController[] characters;
    public  string levelCurrent = "Level";
    void Start()
    {
         characters = FindObjectsOfType<CharacterController>();
       Debug.Log(PlayerPrefs.GetInt("Level"));
       

        retry.onClick.AddListener(RetryGame);
        goManinMenu.onClick.AddListener(GoMainMenu);
        next.onClick.AddListener(NextLevel);

        Time.timeScale = 1;

        if (PlayerPrefs.HasKey("Level"))
        {
            currentLeveIndex = PlayerPrefs.GetInt("Level");
        }
        TM_Manager = FindObjectOfType<TimerManager>();

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevel.text =  currentLevelIndex.ToString();
    }

   
    void Update()
    {
         characters = FindObjectsOfType<CharacterController>();
        if (TM_Manager.isTimeFinish && characters.Length != 0)
        {

            GameLosePanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (characters.Length==0 || characters == null)
        {

            GameWinPanel.SetActive(true);
            Time.timeScale = 0;
        }



    }
    public void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("Level", currentLeveIndex);
        PlayerPrefs.Save();

    }

    public void LoadSavedLevel()
    {
      
            SceneManager.LoadScene(currentLeveIndex);

        


    }


    public void NextLevel()
    {
        currentLeveIndex++;

        if (currentLeveIndex > EditorBuildSettings.scenes.Length - 1)
            currentLeveIndex = 1;
        SaveCurrentLevel();
        LoadSavedLevel();
        


    }


    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void GamePlayButton()
    {
        LoadSavedLevel();
    }

}
