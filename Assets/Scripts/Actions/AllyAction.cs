using UnityEngine;

public class AllyAction : MonoBehaviour
{
    private float actionDuration = 0.5f;
    private float actionTimer = 0;
    private int heal;
    private Collider2D hitbox;

    private void OnEnable()
    {
        actionTimer = actionDuration;
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = true;
    }

    private void Start()
    {
        heal = GetComponentInParent<Ally>().GetHeal();
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
            collision.GetComponent<Player>().AddHealth(heal);
        }
    }
}
