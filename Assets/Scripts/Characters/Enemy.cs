using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float followRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private int score;
    [SerializeField] private GameObject action;

    private Transform _playerTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = FindFirstObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckAttackAndMovement();
        Flip();
        Movement();
    }

    private void CheckHealth()
    {
        if (health.currentHealth <= 0)
        {
            GameManager.instance.AddToScore(score);
            Destroy(this.gameObject);
        }
    }

    private void Flip()
    {
        if (x < 0)
        {
            _spriteRenderer.flipX = true;
            Vector3 pos = action.transform.localPosition;
            action.transform.localPosition = new Vector3(-0.2f, pos.y, pos.z);
        }
        else if (x > 0)
        {
            _spriteRenderer.flipX = false;
            Vector3 pos = action.transform.localPosition;
            action.transform.localPosition = new Vector3(0.2f, pos.y, pos.z);
        }
    }

    public bool CheckFollowRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < followRadius) return true;
        return false;
    }

    public bool CheckAttackRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < attackRadius) return true;
        return false;
    }

    public void CheckAttackAndMovement()
    {
        if (CheckFollowRadius(_playerTransform.position.x, transform.position.x))
        {
            //if player in front of the enemy
            if (_playerTransform.position.x < transform.position.x)
            {
                if (CheckAttackRadius(_playerTransform.position.x, transform.position.x))
                {
                    // ATTACK
                    PerformAction();
                }
                else
                {
                    // MOVE
                    x = -1;
                }

            }
            //if player is behind enemy
            else if (_playerTransform.position.x > transform.position.x)
            {
                if (CheckAttackRadius(_playerTransform.position.x, transform.position.x))
                {
                    // ATTACK
                    PerformAction();
                }
                else
                {
                    // MOVE
                    x = 1;
                }
            }
        }
        else
        {
            // IDLE
            x = 0;
        }
    }

    public override void PerformAction()
    {
        if (!action.activeSelf)
        {
            action.SetActive(true);
        }
    }

    public new void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(BlinkSprite());
    }

    private IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public int GetDamage()
    {
        return this.damage;
    }
}
