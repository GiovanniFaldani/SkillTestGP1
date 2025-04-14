using UnityEngine;

public class Player : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded = false;


    

    public override void PerformAction()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {

    }

    void Update()
    {
        GetInput();
        Movement();
        Jump();
    }

    private void GetInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Jump");
    }

    private void Jump()
    {
        if (isGrounded) _rb.AddForce(new Vector2(0, Mathf.Round(y)) * jumpForce, ForceMode2D.Impulse);
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

}
