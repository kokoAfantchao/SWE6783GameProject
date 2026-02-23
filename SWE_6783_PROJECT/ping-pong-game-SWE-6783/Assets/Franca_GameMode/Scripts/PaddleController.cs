using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public float speed = 8f;

    void Update()
    {
        float move = 0;

        if (Input.GetKey(upKey)) move = 1;
        if (Input.GetKey(downKey)) move = -1;

        transform.Translate(Vector2.up * move * speed * Time.deltaTime);
    }
}
