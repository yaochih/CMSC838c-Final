using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject racket;
    public float ballSpeedMultiplier = 2.0f;

    Vector3 lastFrameVelocity;
    Vector3 currentFrameVelocity;
    Rigidbody rigidbody;
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        lastFrameVelocity = currentFrameVelocity;
        currentFrameVelocity = rigidbody.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.tag == "Racket")
        {
            Debug.Log("hit");
            Vector3 dir = Vector3.Reflect(lastFrameVelocity, collision.contacts[0].normal);
            rigidbody.velocity = dir * ballSpeedMultiplier;
        }
    }
}