using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private float actionDuration = 0.5f;
    private float actionTimer = 0;
    private int damage;
    private Collider2D hitbox;

    private void OnEnable()
    {
        actionTimer = actionDuration;
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = true;
    }

    private void Start()
    {
        damage = GetComponentInParent<Player>().GetDamage();
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    private void Update()
    {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0)
        {
            hitbox.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
