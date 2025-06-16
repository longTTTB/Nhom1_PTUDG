using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject HitBoxEnemy;
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float patrolDistance = 3f; // khoảng cách tuần tra
    [SerializeField] protected Image hpBar;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float detectRadius = 4f;
    [SerializeField] protected GameObject posCircle;
    [SerializeField] protected LayerMask checkLayer;

    protected Animator animator;
    protected float currentHp;
    protected Vector2 startPoint;
    protected Rigidbody2D rb;
    protected bool movingRight = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        startPoint = transform.position;
        UpdateHpBar();
    }

    protected virtual void Update()
    {
        Patrol();
        CheckEnemy();
    }

    void Patrol()
    {
        float moveDir = movingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);

        float distanceMoved = transform.position.x - startPoint.x;

        if (movingRight && distanceMoved >= patrolDistance)
        {
            Flip();
        }
        else if (!movingRight && distanceMoved <= -patrolDistance)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    protected virtual void CheckEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(posCircle.transform.position, detectRadius, checkLayer);
        if (enemies.Length > 0)
        {
            moveSpeed = 0;
            animator.SetBool("isattack", true);
        }
        else
        {
            moveSpeed = 1f;
            animator.SetBool("isattack", false);
        }
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        StartCoroutine(DamageFlash());
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
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
        HitBoxEnemy.SetActive(true);
    }

    public void HitBoxOff()
    {
        HitBoxEnemy.SetActive(false);
    }

    public bool isDie()
    {
        return currentHp <= 0;
    }

    void OnDrawGizmosSelected()
    {
        if (posCircle != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(posCircle.transform.position, detectRadius);
        }
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
