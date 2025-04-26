using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject action;
    private bool isGrounded = false;

    private float _baseSpeed;
    private bool _doBlink = true;


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
        Movement();
        Jump();
        InvincibleBlink();
        PerformAction();
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
        if (isGrounded && y > 0) 
        {
            _rb.AddForce(new Vector2(0, Mathf.Round(y)) * jumpForce, ForceMode2D.Impulse);
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
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            _stateController.ChangeState(_stateController.idleState);
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
        _invincibleTimer = 0.2f;
        StartCoroutine(BlinkSprite());
        _doBlink = false;
    }

    public int GetDamage()
    {
        return this.damage;
    }

}
