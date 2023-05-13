using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject countdownTextObject;
    private TextMeshPro countdownText;
    private bool isready;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        countdownText = countdownTextObject.GetComponent<TextMeshPro>();
        isready = false;
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
        
    }

    public void startgame(){
        countdownTextObject.SetActive(true);
        StartCoroutine(showcountdown());
    }

    private void resumeGame(){
        Time.timeScale = 1;
    }

    IEnumerator showcountdown(){
        countdownText.SetText("3");
        yield return new WaitForSeconds(1);
        countdownText.SetText("2");
        yield return new WaitForSeconds(1);
        countdownText.SetText("1");
        yield return new WaitForSeconds(1);
        countdownTextObject.SetActive(false);
        resumeGame();
    }
}