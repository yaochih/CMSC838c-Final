using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float timeRemaining = 120;
    public float timeStart = 4;
    public GameObject ball;
    public bool timeIsStarting = false;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool isWin = false;

    private void Start()
    {
        // Starts the timer automatically
        // timerIsRunning = true;
        Time.timeScale = 0;
        // set ball's gravity to false
        ball.GetComponent<Rigidbody>().useGravity = false;
    }
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)){
                
                timeIsStarting = true;
                Time.timeScale = 1;
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
                if (isWin){
                    showwin();
                    timeRemaining = 0;
                    timerIsRunning = false;
                }
            }
            else
            {
                showlose();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void startgame(){
        
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
            ball.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void showwin(){
        timeText.text = string.Format("You Win!");
    }

    public void showlose(){
        timeText.text = string.Format("You Lose!");
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