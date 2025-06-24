using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, DamebyPlayer
{
    [SerializeField] protected Image hpBar;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float detectRadius = 4f;
    [SerializeField] protected GameObject posCircle;
    [SerializeField] protected GameObject posAttack;
    [SerializeField] protected LayerMask checkLayer;
    [SerializeField] GameObject WinUi;
    public bool isFlipped = false;
    protected Animator animator;
    protected float currentHp;
    protected Rigidbody2D rb;
    Player player;
    float movespeed = 0;

    public float speed = 5;
    public float attackRange = 4f;

    private bool canAttack = true;
    public float attackCooldown = 3f;

    public GameObject hitBoxEnemy;
    bool isdie = false;
    public Transform posY;
    Vector3 posPlayer;
    public GameObject thunderPrefabs;
    void Start()
    {
        movespeed = speed;
        player = FindAnyObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        UpdateHpBar();
    }
    void Update()
    {
        LookAtPlayer();
        CheckPlayer();
    }
    public void CheckPlayer()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(posCircle.transform.position, detectRadius, checkLayer);
        if (enemies.Length > 0 && canAttack)
        {
            MoveToPlayer();
            animator.SetBool("isrunning", true);
        }
        else
        {
            animator.SetBool("isrunning", false);
        }
    }

    public void MoveToPlayer()
    {
        Collider2D[] attackplayer = Physics2D.OverlapCircleAll(posAttack.transform.position, attackRange, checkLayer);
        if (attackplayer.Length > 0 && canAttack || player.transform.position.x == rb.position.x)
        {
            float posplayerX = attackplayer[Random.Range(0, attackplayer.Length)].transform.position.x;
            posPlayer = new Vector3(posplayerX+9f, posY.position.y,transform.position.z);
            int skill = Random.Range(0, 3);
            switch (skill)
            {
                case 0:
                    animator.SetTrigger("isattack");
                    break;
                case 1:
                    animator.SetTrigger("isattack2");
                    break;
                case 2:
                    currentHp += 100;
                    currentHp = Mathf.Min(currentHp, maxHp);
                    UpdateHpBar();
                    break;
            }
            canAttack = false;
            speed = 0f; // Dừng di chuyển khi tấn công
            StartCoroutine(AttackCooldown());
        }
        else if (attackplayer.Length <= 0)
        {
            speed = movespeed; // Tiếp tục di chuyển
        }
        if (speed > 0f)
        {
            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // Cho phép tấn công lại
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (player != null)
        {
            if (transform.position.x > player.transform.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.transform.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
    }
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Màu đỏ để dễ thấy
        Gizmos.DrawWireSphere(posCircle.transform.position, detectRadius);
        Gizmos.DrawWireSphere(posAttack.transform.position, attackRange);
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        StartCoroutine(DamageFlash());
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();

        if (currentHp <= 0 && isdie == false)
        {
            Die();
            isdie = true;
            WinUi.SetActive(true);
            
        }
    }
    private void Die()
    {
        animator.SetTrigger("isdie");
        Destroy(gameObject, 1f);
        
    }

    public void HitBoxOn()
    {
        hitBoxEnemy.SetActive(true);
    }

    public void HitBoxOff()
    {
        hitBoxEnemy.SetActive(false);
    }
    IEnumerator DamageFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f); // thời gian nháy
        sr.color = originalColor;
    }
    public void OnAttack()
    {
        Destroy(Instantiate(thunderPrefabs, posPlayer, Quaternion.identity), 1f);
    }
}
