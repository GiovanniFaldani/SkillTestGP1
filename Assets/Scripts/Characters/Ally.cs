using UnityEngine;

public class Ally : Character
{
    [SerializeField] private float followRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private GameObject action;
    [SerializeField] private int healing;

    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = FindFirstObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackAndMovement();
        Flip();
        Movement();
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
        // heal the player
        if (!action.activeSelf)
        {
            action.SetActive(true);
        }
    }

    public int GetHeal()
    {
        return healing;
    }
}
