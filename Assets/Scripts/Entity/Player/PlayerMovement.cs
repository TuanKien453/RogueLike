using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;
[RequireComponent(typeof(Rigidbody2D), typeof(StatsComponent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform visualRoot;

    private Rigidbody2D rb;
    private StatsComponent stats;
    private SpriteRenderer spriteRenderer;

    private float moveInput;
    private float speed;
    private float jumpForce;

    public bool isFacingRight = true;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<StatsComponent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateStats();
        CheckGroundAndJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void UpdateStats()
    {
        speed = stats.GetStats(StatType.MoveSpeed);
        jumpForce = stats.GetStats(StatType.JumpForce);
    }

    private void HandleMovement()
    {
        moveInput = InputManager.Instance.GetInputX();
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput > 0 && !isFacingRight)
            FaceRight();
        else if (moveInput < 0 && isFacingRight)
            FaceLeft();
    }

    private void CheckGroundAndJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && InputManager.Instance.ISPressJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FaceRight()
    {
        isFacingRight = true;
        spriteRenderer.flipX = false;
        visualRoot.localScale = new Vector3(1, 1, 1);
    }

    private void FaceLeft()
    {
        isFacingRight = false;
        spriteRenderer.flipX = true;
        visualRoot.localScale = new Vector3(-1, 1, 1);
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