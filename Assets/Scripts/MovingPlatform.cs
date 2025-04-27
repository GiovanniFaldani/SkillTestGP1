using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float oscillation;

    private Vector2 center;
    private Rigidbody2D rb;

    private void Awake()
    {
        center = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(center.x + oscillation * Mathf.Sin(Time.timeSinceLevelLoad * speed), center.y));
    }
}
