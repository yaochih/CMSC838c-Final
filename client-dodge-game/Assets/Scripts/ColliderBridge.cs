using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBridge : MonoBehaviour
{
    Player _listener;
    public void Initialize(Player l){
        _listener = l;
    }
    void OnCollisionEnter(Collision collision){
        _listener.OnCollisionEnter(collision);
    }
    void OnTriggerEnter(Collider other){
        _listener.OnTriggerEnter(other);
    }
}