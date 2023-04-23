using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject hurt_effect;
    
    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponentInChildren<Collider>();
        Debug.Log(collider.name);
        if (collider.gameObject != gameObject){
            ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
            cb.Initialize(this);
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Bullet"){
            if(!hurt_effect.activeSelf){
                StartCoroutine(showeffect());
            }
        }
    } 

    public void OnTriggerEnter(Collider other){

    }

    IEnumerator showeffect(){
        hurt_effect.SetActive(true);
        yield return new WaitForSeconds(2);
        hurt_effect.SetActive(false);
    }
}
