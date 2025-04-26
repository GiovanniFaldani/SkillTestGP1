using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private float actionDuration = 0.5f;
    private float actionTimer = 0;
    private int damage;
    private Collider2D hitbox;

    private StateController _stateController;

    private void OnEnable()
    {
        actionTimer = actionDuration;
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = true;
    }

    private void Start()
    {
        damage = GetComponentInParent<Enemy>().GetDamage();
        _stateController = GetComponentInParent<StateController>();
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    private void Update()
    {
        actionTimer -= Time.deltaTime;
        if (actionTimer < 0)
        {
            _stateController.ChangeState(_stateController.idleState);
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

