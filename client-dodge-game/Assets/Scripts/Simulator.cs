using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move() {
        if(Input.GetKey(KeyCode.UpArrow)) {
            this.transform.position += this.transform.forward * 0.01f;
        }
        else if(Input.GetKey(KeyCode.DownArrow)) {
            this.transform.position -= this.transform.forward * 0.01f;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Rotate(new Vector3(0, -0.1f, 0));
        }
        else if(Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Rotate(new Vector3(0, 0.1f, 0));
        }
    }
}
