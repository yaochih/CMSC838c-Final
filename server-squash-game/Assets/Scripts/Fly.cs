using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Fly : MonoBehaviour {

    public float speed = 15.0f; // camera movement speed
    private OVRCameraRig cameraRig;

    void Start () {
        cameraRig = GetComponent<OVRCameraRig>();
    }

    void Update () {
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector3 forward = cameraRig.centerEyeAnchor.forward;
        Vector3 right = cameraRig.centerEyeAnchor.right;
        forward.Normalize();
        right.Normalize();
        transform.position += (forward * thumbstick.y + right * thumbstick.x) * speed * Time.deltaTime;
    }
    

}