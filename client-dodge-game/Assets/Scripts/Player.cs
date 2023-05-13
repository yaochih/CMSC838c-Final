using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject hurt_effect;
    public GameObject GameEnv;
    public Image HealthBarImage;
    public List<Color> colors = new List<Color>();
    private AudioSource hurt_AS;
    // Start is called before the first frame update
    void Start()
    {
        hurt_AS = GetComponent<AudioSource>();
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
            SetHealthBarValue(GetHealthBarValue() - 0.1f);
            if(!hurt_effect.activeSelf){
                hurt_AS.Play();
                StartCoroutine(showeffect());
                StartCoroutine(sendhaptic());
            }
        }
    } 

    public void OnTriggerEnter(Collider other){

    }

    IEnumerator sendhaptic(){
        OVRInput.SetControllerVibration(1, 0.5f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(1, 0.5f, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    IEnumerator showeffect(){
        hurt_effect.SetActive(true);
        yield return new WaitForSeconds(2);
        hurt_effect.SetActive(false);
    }

    public void SetHealthBarValue(float value){
        HealthBarImage.fillAmount = value;
        if(HealthBarImage.fillAmount < 0.2f){
            SetHealthBarColor(colors[2]);
        }
        else if(HealthBarImage.fillAmount < 0.5f){
            SetHealthBarColor(colors[1]);
        }
        else{
            Debug.Log(colors[0]);
            SetHealthBarColor(colors[0]);
        }
        if(HealthBarImage.fillAmount == 0.0f){
            GameEnv.GetComponent<GameController>().showlose();
        }
    }

    public void SetHealthBarColor(Color healthColor){
        HealthBarImage.color = healthColor;
    }

    public float GetHealthBarValue(){
        return HealthBarImage.fillAmount;
    }
}
