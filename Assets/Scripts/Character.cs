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
    [SerializeField] private CharacterData characterData;

    // internals
    private Health health = new Health();
    private float moveSpeed;
    private int damage;

    // component shorthands
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // movement
    [Range(-1,1)] private float x; 

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = new Health(characterData.maxHealth, false);
        moveSpeed = characterData.moveSpeed;
        damage = characterData.damage;
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
