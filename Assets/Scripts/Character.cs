using UnityEngine;

public struct Health
{
    public int maxHealth;
    public int currentHealth;
    public bool invincibe;

    public Health(int maxHealth, bool invincible)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.invincibe = invincible;
    }
}

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;

    // internals
    protected Health health = new Health();
    protected float moveSpeed;
    protected int damage;

    // component shorthands
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;

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
    }

    private void Start()
    {
        
    }

    public void Movement()
    {
        _rb.linearVelocity = new Vector2(x * moveSpeed, _rb.linearVelocity.y);
    }

    public void AddToHealth(int addMe)
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

    public abstract void PerformAction();

}
