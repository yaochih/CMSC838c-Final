using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject countdownTextObject;
    public GameObject canvas;
    private TextMeshPro countdownText;
    public Text timeText;
    private bool isready;
    private bool isstart;
    public float timeRemaining = 120;
    private Transform txtobject;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        countdownText = countdownTextObject.GetComponent<TextMeshPro>();
        isready = false;
        isstart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isready){
            if(OVRInput.GetDown(OVRInput.Button.One)){
                isready = true;
                startgame();
            }
        } 
        if(isstart){
            if (timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
                starttimer();
            } else{
                showwin();
            }
        }
        
    }

    public void startgame(){
        canvas.SetActive(false);
        countdownTextObject.SetActive(true);
        StartCoroutine(showcountdown());
    }

    private void starttimer(){
        float minutes = Mathf.FloorToInt(timeRemaining / 60);  
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void resumeGame(){
        Time.timeScale = 1;
    }

    IEnumerator showcountdown(){
        resumeGame();
        countdownText.SetText("3");
        yield return new WaitForSeconds(1);
        countdownText.SetText("2");
        yield return new WaitForSeconds(1);
        countdownText.SetText("1");
        yield return new WaitForSeconds(1);
        countdownTextObject.SetActive(false);
        isstart = true;
    }

    public void showlose(){
        txtobject = canvas.transform.Find("ready");
        txtobject.GetComponent<Text>().text = "You Lose!";
        canvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void showwin(){
        txtobject = canvas.transform.Find("ready");
        txtobject.GetComponent<Text>().text = "You Win!";
        canvas.SetActive(true);
        Time.timeScale = 0;
    }
}