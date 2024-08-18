using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 15f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing) return;  // Không di chuyển khi đang lướt

        Move();
        Jump();
        Dash();
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Giảm tốc độ di chuyển trên không để tăng độ kiểm soát
        if (!isGrounded)
        {
            moveVelocity.x *= 0.8f;  // Điều chỉnh giá trị này để phù hợp
        }

        rb.velocity = moveVelocity;

        // Quay mặt nhân vật theo hướng di chuyển
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }


}
