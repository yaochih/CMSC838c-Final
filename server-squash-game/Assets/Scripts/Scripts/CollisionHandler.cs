using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject racket;
    public float ballSpeedMultiplier = 2.0f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 racketVelocity = racket.GetComponent<Rigidbody>().velocity;
            ballRigidbody.velocity = (ballRigidbody.velocity + racketVelocity) * ballSpeedMultiplier;
        }
    }
}