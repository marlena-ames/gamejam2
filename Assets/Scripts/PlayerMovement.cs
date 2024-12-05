using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlayer1 = true; // Distinguish between Player 1 and Player 2

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Check controls based on player identity
        if (isPlayer1)
        {
            horizontal = Input.GetAxisRaw("Horizontal1"); // Custom axis for Player 1 (WASD)
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal2"); // Custom axis for Player 2 (Arrow Keys)
        }

        // Jump control based on player identity
        if (isPlayer1 && Input.GetButtonDown("Jump1") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (!isPlayer1 && Input.GetButtonDown("Jump2") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Let go of jump for both players
        if (Input.GetButtonUp(isPlayer1 ? "Jump1" : "Jump2") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}