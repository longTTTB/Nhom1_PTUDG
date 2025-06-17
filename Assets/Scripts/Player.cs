using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image hpBar;
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
    public GameObject hitbox;
    private float currentHp = 100f;
    public float maxHp = 500f;
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
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
    public void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
    public void MovePlayer()
    {

        float moveInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(moveInput * moveSpeed, rigidbody2D.velocity.y);
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
            hitbox.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX= true;
            hitbox.transform.localScale = new Vector3(-1, 1, 1);
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
        animator.SetTrigger("isjumping");
    }
    private void UpdateAnimation()
    {
        isRunning = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f;
        animator.SetBool("isruning", isRunning);
    }
    private void OnAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGroundCheck && !isRunning)
        {
            animator.SetTrigger("isattack");
        }

    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        StartCoroutine(DamageFlash());
        UpdateHpBar();
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public void HitBoxOn()
    {
        hitbox.SetActive(true);
    }
    public void HitBoxOff()
    {
        hitbox.SetActive(false);
    }
    IEnumerator DamageFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f); // thời gian nháy
        sr.color = originalColor;
    }
}
