using UnityEngine;

public class EnemyAction : MonoBehaviour
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
        damage = GetComponentInParent<Enemy>().GetDamage();
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}

