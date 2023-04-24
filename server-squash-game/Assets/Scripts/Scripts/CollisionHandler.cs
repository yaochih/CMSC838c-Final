using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject racket;
    public float ballSpeedMultiplier = 2.0f;
    private Vector3 racketVelocity = Vector3.zero;


    Vector3 lastFrameVelocity;
    Vector3 currentFrameVelocity;
    Vector3 previousRacketPosition;
    Vector3 currentRacketPosition;
    Rigidbody rigidbody;
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        currentRacketPosition = racket.transform.position;
    }

    void Update() {
        lastFrameVelocity = currentFrameVelocity;
        currentFrameVelocity = rigidbody.velocity;
        previousRacketPosition = currentRacketPosition;
        currentRacketPosition = racket.transform.position;
        racketVelocity = (currentRacketPosition - previousRacketPosition)/Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.tag == "Racket")
        {
            Debug.Log("hit");
            Vector3 dir = Vector3.Reflect(lastFrameVelocity, collision.contacts[0].normal);
            rigidbody.velocity = dir * ballSpeedMultiplier + racketVelocity;
            // rigidbody.velocity
        }
    }
}