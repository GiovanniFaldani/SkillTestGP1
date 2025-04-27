using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;

    // internals
    [SerializeField] protected Health health = new Health();
    protected float moveSpeed;
    protected int damage;

    // timers
    [SerializeField] protected float _invincibleTimer;
    protected float _speedModTimer;

    // component shorthands
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected StateController _stateController;

    // movement
    [Range(-1,1)] protected float x;
    [Range(-1, 1)] protected float y;

    void Awake()
    {
        // load characterData
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        health = new Health(characterData.maxHealth, false);
        moveSpeed = characterData.moveSpeed;
        damage = characterData.damage;
        _animator.runtimeAnimatorController = characterData.animController;
        _stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        
    }

    public void Movement()
    {
        _rb.linearVelocityX = x * moveSpeed * Time.fixedDeltaTime;
    }

    public void AddHealth(int addMe)
    {
        if(health.currentHealth + addMe > health.maxHealth)
        {
            health.currentHealth = health.maxHealth;
        }
        else
        {
            health.currentHealth += addMe;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!health.invincible)
        {
            health.currentHealth -= damage;
        }
    }

    public void SetInvincibile(bool state, float duration)
    {
        health.invincible = state;
        _invincibleTimer = duration;
    }

    public abstract void PerformAction();

}
