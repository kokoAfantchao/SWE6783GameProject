using UnityEngine;

public class AIPaddleController : MonoBehaviour
{
    public float speed = 6f;
    public Transform ball;
    public float reactionDelay = 0.1f;

    private float targetY;

    void Update()
    {
        if (ball == null) return;

        // Smoothly follow ball Y position
        targetY = Mathf.Lerp(targetY, ball.position.y, reactionDelay);

        // Keep paddle inside visible camera range
        float clampedY = Mathf.Clamp(targetY, -10f, 10f);

        Vector3 newPos = transform.position;
        newPos.y = Mathf.MoveTowards(newPos.y, clampedY, speed * Time.deltaTime);

        transform.position = newPos;
    }
}