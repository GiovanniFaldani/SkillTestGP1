using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float followRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private int score;
    [SerializeField] private float rotateSpeed;
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
    }

    private void FixedUpdate()
    {
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

    private void FacePlayer()
    {
        this.transform.rotation = Quaternion.Lerp(
            this.transform.rotation,
            Quaternion.LookRotation(_playerTransform.position - this.transform.position),
            rotateSpeed
            ) * Quaternion.Euler(0, 90, 0);
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
            // Stop rotating
            this.transform.rotation = Quaternion.Euler(0,0,0);
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
                    _stateController.ChangeState(_stateController.moveState);
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
                    _stateController.ChangeState(_stateController.moveState);
                }
            }
        }
        else
        {
            // IDLE
            x = 0;
            FacePlayer();
            _stateController.ChangeState(_stateController.idleState);
        }
    }

    public override void PerformAction()
    {
        // Attack the player
        if (!action.activeSelf)
        {
            _stateController.ChangeState(_stateController.actionState);
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
