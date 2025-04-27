using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 direction;

    void Start()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void Update()
    {
        MoveInDirection();
    }

    private void MoveInDirection()
    {
        Vector2 currPos = transform.position;
        this.transform.position = Vector2.MoveTowards(currPos, currPos + direction, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bounce with reflect
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
    }
}
