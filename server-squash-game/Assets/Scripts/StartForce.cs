using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 startForce;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(startForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
