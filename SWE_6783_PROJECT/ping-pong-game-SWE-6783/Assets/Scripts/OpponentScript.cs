using UnityEngine;

public class OpponentScript : MonoBehaviour
{
public float speed = 5f;
    public GameObject ball; // Drag the Ball object here in the Inspector
    
    // Limits how far the paddle can go up or down
    public float yBoundary = 4.5f;

    void Start()
    {
        yBoundary = Camera.main.orthographicSize - (transform.localScale.y / 2);
    }

    void Update()
    {
        if (ball != null)
        {
            // 1. Get the current positions
            float ballY = ball.transform.position.y;
            float paddleY = transform.position.y;

            // 2. Decide which way to move to reach the ball
            if (ballY > paddleY + 0.2f) // Added a small "deadzone" to prevent jitter
            {
                Move(Vector2.up);
            }
            else if (ballY < paddleY - 0.2f)
            {
                Move(Vector2.down);
            }
        }
    }

    void Move(Vector2 direction)
    {
        // Move the paddle based on speed and time
        transform.Translate(direction * speed * Time.deltaTime);

        // Clamp the position so it doesn't fly off the screen
        float clampedY = Mathf.Clamp(transform.position.y, -yBoundary, yBoundary);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
