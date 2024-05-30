using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;

    private float timer = 0;
    [SerializeField] private float duration = 120;

    public bool isTimeFinish = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timerText != null)
        {

            int minutes = Mathf.FloorToInt(duration - timer) / 60;
            int seconds = Mathf.FloorToInt(duration - timer) % 60;

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }


        
        if (timer >= duration&& isTimeFinish==false)
        {
            isTimeFinish = true;
        }
    }
}
