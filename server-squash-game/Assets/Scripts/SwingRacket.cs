using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRacket : MonoBehaviour {
    public OVRCameraRig cameraRig;
        public GameObject ball;
        public float racketSpeed = 30f;

        private bool isSwinging = false;
        private float swingStartTime;
        private Vector3 prevPos;

        void Start() {

        }

        void Update () {
            OVRInput.Update();

            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) {
                isSwinging = true;
                swingStartTime = Time.time;
                prevPos = cameraRig.centerEyeAnchor.position;
            }

            if (isSwinging) {
                // Vector3 newPos = cameraRig.centerEyeAnchor.position;
                // Vector3 newPos = cameraRig.rightHandAnchor.position;
                // get ball's position
                Vector3 newPos = ball.transform.position;
                newPos.y += 0.2f;

                // Vector3 newPos = new Vector3(1.0f, 0.2f, 0.3f);
                
                // Vector3 planeTanget = Vector3.Cross(prevPos - newPos, transform.forward);
                // Vector3 planeNormal = Vector3.Cross(planeTanget, prevPos - newPos);
                // Vector3 reflectedPos = Vector3.Reflect(transform.forward, planeNormal);
                // reflectedPos.Normalize();
                // Rigidbody rb = ball.GetComponent<Rigidbody>();
                // rb.velocity = reflectedPos * racketSpeed;


                Vector3 velocity = (newPos - prevPos) / Time.deltaTime;
                Vector3 force = velocity * 1.0f; // Adjust this to control the strength of the swing
                // clip the force
                // if (force.magnitude > 50.0f) {
                //     force = force.normalized * 50.0f;
                // }
                // ball.GetComponent<Rigidbody>().AddForce(force);

                Rigidbody rb = ball.GetComponent<Rigidbody>();
                // clip the velocity
                if (velocity.magnitude > 50.0f) {
                    velocity = velocity.normalized * 50.0f;
                }
                rb.velocity = velocity;
                
                

                if (Time.time - swingStartTime > 0.2f) {
                    isSwinging = false;
                }
            }
        // Vector3 prevPos = new Vector3(0.0f, 0.0f, 0.0f);
        // Vector3 newPos = new Vector3(1.0f, 0.2f, 0.3f);
        // // prevPos = cameraRig.leftHandAnchor.position;
        // // Vector3 newPos = cameraRig.rightHandAnchor.position;
        // Debug.Log("prevPos: " + prevPos);
        // Debug.Log("newPos: " + newPos);
        // Vector3 velocity = (newPos - prevPos) / Time.deltaTime;
        // Vector3 force = velocity * 0.05f; // Adjust this to control the strength of the swing
        // ball.GetComponent<Rigidbody>().AddForce(force);
    }
}

// public class SwingRacket : MonoBehaviour
// {
//     public float racketSpeed = 10f;
//     public GameObject ball;
//     private Rigidbody rb;
//     private bool isRacketInMotion = false;
//     private OVRCameraRig cameraRig;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         cameraRig = FindObjectOfType<OVRCameraRig>();
//     }

//     void Update()
//     {
//         if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && !isRacketInMotion)
//         {
//             // Get the direction of the swing
//             Vector3 swingDirection = cameraRig.centerEyeAnchor.position - transform.position;
//             // Normalize the direction
//             swingDirection.Normalize();
//             // Set the velocity of the racket to be in the direction of the swing
//             rb.velocity = swingDirection * racketSpeed;
//             // Set isRacketInMotion to true
//             isRacketInMotion = true;
//         }
//     }

//     void OnCollisionEnter(Collision collision)
//     {
//         if (collision.gameObject == ball)
//         {
//             // Reset the velocity of the racket
//             rb.velocity = Vector3.zero;
//             // Set isRacketInMotion to false
//             isRacketInMotion = false;
//         }
//     }
// }


