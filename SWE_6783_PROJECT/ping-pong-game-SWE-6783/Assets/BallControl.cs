using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BallControl : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed = 30f;
	public float bounceInfluence = 0.5f;
	private AudioSource bounceAudio;

	void GoBall() {
    // Determine the horizontal direction (1 or -1)
    float directionX = Random.Range(0, 2) < 1 ? 1f : -1f;
    
    // Set the velocity directly
    // .normalized ensures the vector's length is 1, then we multiply by speed
    rb2d.linearVelocity = new Vector2(directionX, -0.75f).normalized * speed;
}
public void Launch()
{
    rb2d.linearVelocity = Vector2.zero;
    float directionX = Random.Range(0, 2) < 1 ? 1f : -1f;
    rb2d.linearVelocity = new Vector2(directionX, -0.75f).normalized * speed;
}

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		bounceAudio = GetComponent<AudioSource> ();
		//Invoke("GoBall", 2);
	}

	void FixedUpdate () {
		// Limit the ball's speed to prevent it from going too fast
		rb2d.linearVelocity = Vector2.ClampMagnitude(rb2d.linearVelocity, speed);
	}

	void ResetBall() {
		rb2d.linearVelocity = new Vector2 (0, 0);
		transform.position = Vector2.zero;
	}

	void RestartGame() {
		ResetBall ();
		Invoke ("GoBall", 1);
	}
void OnCollisionEnter2D(Collision2D coll) {
    if (coll.collider.CompareTag("Player")) {
        // 1. Determine horizontal direction (Away from paddle)
        float directionX = (transform.position.x > coll.transform.position.x) ? 1f : -1f;

        // 2. Calculate the base vertical angle based on where it hit
        float hitPoint = (transform.position.y - coll.transform.position.y) / coll.collider.bounds.size.y;

        // 3. Add a dash of randomness to the Y axis
        float randomAngle = Random.Range(-bounceInfluence, bounceInfluence);
        float finalY = hitPoint + randomAngle;

        // 4. Normalize and apply velocity
        // .normalized ensures the ball doesn't speed up just because it's going diagonal
        Vector2 newDirection = new Vector2(directionX, finalY).normalized;
        rb2d.linearVelocity = newDirection * speed;
    }
	bounceAudio.Play();
}
}
