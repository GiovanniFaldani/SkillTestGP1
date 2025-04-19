using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded = false;
    
    private float _baseSpeed;
    private float _speedModTimer;

    public override void PerformAction()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        _baseSpeed = moveSpeed;
    }

    void Update()
    {
        UpdateTimers();
        GetInput();
        Flip();
        Movement();
        Jump();
    }

    private void UpdateTimers()
    {
        if (_speedModTimer > 0)
        {
            _speedModTimer -= Time.deltaTime;
            if (_speedModTimer < 0)
            {
                moveSpeed = _baseSpeed;
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
        if (x < 0) _spriteRenderer.flipX = true;
        else _spriteRenderer.flipX = false;
        
    }

    private void Jump()
    {
        if (isGrounded && y > 0) 
        {
            _rb.AddForce(new Vector2(0, Mathf.Round(y)) * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")){
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    public void ModifySpeed(float speedModifier, float duration)
    {
        moveSpeed *= speedModifier;
        _speedModTimer = duration;
    }

    private void InvincibleBlink()
    {

    }

}
