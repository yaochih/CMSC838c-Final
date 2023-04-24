using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    Renderer renderer;
    public Color defaultColor;
    public Color hitColor;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", defaultColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) {
        //hurt_AS.Play();
        StartCoroutine(showeffect());
    } 

    IEnumerator showeffect(){
        renderer.material.SetColor("_Color", hitColor);
        yield return new WaitForSeconds(0.5f);
        renderer.material.SetColor("_Color", defaultColor);
    }
}
