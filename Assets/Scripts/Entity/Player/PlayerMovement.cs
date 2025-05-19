using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;
[RequireComponent(typeof(Rigidbody2D),typeof(StatsComponent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.1f;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform visualRoot;

    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private Rigidbody2D rb;
    private float moveInput;
    private StatsComponent stats;

    private float speed;
    private float jumpForce;
    public bool isFacingRight = true;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<StatsComponent>();
    }

    void Update()
    {
        UpdateStats();
        CheckGroundAndJump();
    }
    private void FixedUpdate()
    {
        HandleMovementInput();
    }
    private void UpdateStats()
    {
        speed = stats.GetStats(StatType.MoveSpeed);
        jumpForce = stats.GetStats(StatType.JumpForce);
    }

    private void HandleMovementInput()
    {
        moveInput = InputManager.Instance.GetInputX();
        rb.velocity = new Vector2(moveInput*speed, rb.velocity.y);
        if (moveInput < 0 && isFacingRight)
        {
            FacingLeft();
        }
        else if (moveInput > 0 && !isFacingRight)
        {
            FacingRight();
        }
    }

    private void CheckGroundAndJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && InputManager.Instance.ISPressJump())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void FacingLeft()
    {
        isFacingRight = false;
        spriteRenderer.flipX = true;
        visualRoot.transform.localScale = new Vector3(-1,1,1);
    }

    private void FacingRight()
    {
        isFacingRight = true;
        spriteRenderer.flipX = false;
        visualRoot.transform.localScale = new Vector3(1, 1, 1);
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}