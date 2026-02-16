using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BallControl : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed = 30f;
	public float bounceInfluence = 0.5f;

	void GoBall() {
		float rand = Random.Range (0, 2);
		if (rand < 1) {
			rb2d.AddForce (new Vector2 (20, -15));
		} else {
			rb2d.AddForce (new Vector2 (-20, -15));
		}
	}

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		Invoke ("GoBall", 2);
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
		if (coll.collider.CompareTag ("Player")) {
			Vector2 vel;
			vel.x = rb2d.linearVelocity.x;
			vel.y = (rb2d.linearVelocity.y / 2.0f) + (coll.collider.attachedRigidbody.linearVelocity.y / 3.0f);
			vel.y *= bounceInfluence;
			rb2d.linearVelocity = vel;
		}
	}

}
