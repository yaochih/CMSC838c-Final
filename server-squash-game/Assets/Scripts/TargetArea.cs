using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetArea : MonoBehaviour
{
    Renderer renderer;
    public Color defaultColor;
    public Color hitColor;
    private AudioSource cheers;
    public Text scoreText;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", defaultColor);
        cheers = GetComponent<AudioSource>();
        scoreText.text = string.Format("Score: {0}", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) {
        //hurt_AS.Play();
        StartCoroutine(showeffect());
        cheers.Play();
        score += 1;
        scoreText.text = string.Format("Score: {0}", score);
    } 

    IEnumerator showeffect(){
        renderer.material.SetColor("_Color", hitColor);
        yield return new WaitForSeconds(0.5f);
        renderer.material.SetColor("_Color", defaultColor);
    }
}
