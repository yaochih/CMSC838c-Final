using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float timeRemaining = 120;
    public float timeStart = 4;
    public bool timeIsStarting = false;
    public bool timerIsRunning = false;
    public Text timeText;
    private void Start()
    {
        // Starts the timer automatically
        // timerIsRunning = true;
        Time.timeScale = 0;
    }
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)){
                
                timeIsStarting = true;
                
        }
        startgame();
        // if (timeStart > 0 && timeIsStarting)
        // {
        //         DisplayReady(timeStart);
        //         timeStart -= Time.deltaTime;
        // }
        // if (timeStart < 0)
        // {
        //     timeStart = 0;
        //     timeIsStarting = false;
        //     timeText.text = string.Format("Start!");
        //     timerIsRunning = true;
        // }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void startgame(){
        Time.timeScale = 1;
        if (timeStart > 0 && timeIsStarting)
        {
                DisplayReady(timeStart);
                timeStart -= Time.deltaTime;
        }
        if (timeStart < 0)
        {
            timeStart = 0;
            timeIsStarting = false;
            timeText.text = string.Format("Start!");
            timerIsRunning = true;
        }
    }

    void DisplayReady(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:0}!", seconds);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("Time left {0:00}:{1:00}", minutes, seconds);
    }
}