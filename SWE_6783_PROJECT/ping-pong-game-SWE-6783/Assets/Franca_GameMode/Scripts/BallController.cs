using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    public void Launch()
    {
        float x = Random.value > 0.5f ? 1 : -1;
        float y = Random.Range(-0.3f, 0.3f);

        Vector2 direction = new Vector2(x * 1.2f, y).normalized;
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Paddle"))
        {
            // Keep speed constant — only change direction
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }
}
