using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedBullet : MonoBehaviour
{

    Vector3 prePos, velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        velocity = (transform.position - prePos) / Time.deltaTime;
        prePos = transform.position;
        
        Debug.Log(velocity);
        if(velocity.z > 0) {
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        } else {
            GetComponent<Collider>().enabled = true;
            GetComponent<Renderer>().enabled = true;
        }
    }
}
