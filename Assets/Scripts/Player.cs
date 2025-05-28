using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    Animator animator;
    public float damage;
    Rigidbody2D rigidbody2D;
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.2f;
    SpriteRenderer spriteRenderer;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGroundCheck;
    bool isRunning;
    bool canDoubleJump;
    bool isJumping;
    bool isAttack;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        UpdateAnimation();
        OnAttack();
    }
    public void MovePlayer()
    {

        float moveInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(moveInput * moveSpeed, rigidbody2D.velocity.y);
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        isGroundCheck = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGroundCheck && Input.GetButtonDown("Jump"))
        {
            Jump();
            canDoubleJump = true;
        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            Jump();
            canDoubleJump = false;
        }
    }
    public void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
    }
    private void UpdateAnimation()
    {
        isJumping = !isGroundCheck;
        isRunning = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f;
        animator.SetBool("isrun", isRunning);
        animator.SetBool("isjump", isJumping);
    }
    private void OnAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGroundCheck && !isRunning)
        {
            animator.SetTrigger("isattack");
        }
        
    }
}
