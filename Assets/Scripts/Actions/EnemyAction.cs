using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private float actionDuration = 0.5f;
    private float actionTimer = 0;
    private int damage;

    private void OnEnable()
    {
        actionTimer = actionDuration;
    }

    private void Start()
    {
        damage = GetComponentInParent<Enemy>().GetDamage();
    }

    private void Update()
    {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0)
        {
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

