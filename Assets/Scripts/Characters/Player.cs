using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject action;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] float xSpeedCap;
    [SerializeField] float ySpeedCap;


    private float _baseSpeed;
    private bool _doBlink = true;
    private Vector2 _surfaceNormal = Vector2.up;


    void Start()
    {
        _baseSpeed = moveSpeed;
    }

    void Update()
    {
        CheckHealth();
        UpdateTimers();
        GetInput();
        Flip();
        Jump();
        InvincibleBlink();
        PerformAction();
    }
    private void FixedUpdate()
    {
        CapSpeed();
        Movement();
    }

    private void CapSpeed()
    {
        if(_rb.linearVelocityX > xSpeedCap) _rb.linearVelocityX = xSpeedCap;
        if (_rb.linearVelocityX < -xSpeedCap) _rb.linearVelocityX = -xSpeedCap;
        if(_rb.linearVelocityY > ySpeedCap) _rb.linearVelocityY = ySpeedCap;
    }

    private void CheckHealth()
    {
        if (health.currentHealth <= 0)
        {
            GameManager.instance.GameOver();
            Time.timeScale = 0f;
        }
    }

    private void UpdateTimers()
    {
        if (_speedModTimer > 0)
        {
            _speedModTimer -= Time.deltaTime;
            if (_speedModTimer <= 0)
            {
                moveSpeed = _baseSpeed;
            }
        }
        if (_invincibleTimer > 0)
        {
            health.invincible = true;
            _invincibleTimer -= Time.deltaTime;
            if(_invincibleTimer <= 0)
            {
                health.invincible = false;
            }
        }
    }

    private void GetInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Jump");
    }

    private void Flip()
    {
        if (x < 0)
        {
            _spriteRenderer.flipX = true;
            Vector3 pos = action.transform.localPosition;
            action.transform.localPosition = new Vector3(-0.2f, pos.y, pos.z);
            _stateController.ChangeState(_stateController.moveState);
        }
        else if (x > 0)
        {
            _spriteRenderer.flipX = false;
            Vector3 pos = action.transform.localPosition;
            action.transform.localPosition = new Vector3(0.2f, pos.y, pos.z);
            _stateController.ChangeState(_stateController.moveState);
        }
        else
        {
            _stateController.ChangeState(_stateController.idleState);
        }
    }

    private void Jump()
    {
        if (isGrounded && y > 0 && (Vector2.Dot(_surfaceNormal, Vector2.up) >= 0.5f)) // As long as the slope is less than 45°, allow jump
        {
            // Jump in the direction of the surface normal
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            _stateController.ChangeState(_stateController.jumpState);
        }
    }

    public override void PerformAction()
    {
        if(Input.GetAxis("Fire1") > 0)
        {
            _stateController.ChangeState(_stateController.actionState);
            action.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && Vector2.Dot(collision.contacts[0].normal, Vector2.up) >= 0.5f) // Only return grounded on inclines smaller than 45°
        {
            isGrounded = true;
            _stateController.ChangeState(_stateController.idleState);
            _surfaceNormal = collision.contacts[0].normal;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
            _stateController.ChangeState(_stateController.jumpState);
        }
    }

    public void ModifySpeed(float speedModifier, float duration)
    {
        moveSpeed *= speedModifier;
        _speedModTimer = duration;
    }

    private void InvincibleBlink()
    {
        if(_invincibleTimer > 0 && _doBlink)
        {
            // blink sprite
            StartCoroutine(BlinkSprite());
            _doBlink = false;
        }
    }

    private IEnumerator BlinkSprite()
    {
        float t = _invincibleTimer;
        float blinkTimer = 0.025f;
        while (t > 0)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkTimer);
            t -= blinkTimer + Time.deltaTime;
        }
        _spriteRenderer.enabled = true;
        _doBlink = true;
    }

    public new void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!health.invincible)
        {
            _invincibleTimer = 0.2f;
            StartCoroutine(BlinkSprite());
            _doBlink = false;
        }
    }

    public int GetDamage()
    {
        return this.damage;
    }

    public int GetCurrentHP()
    {
        return health.currentHealth;
    }

}
