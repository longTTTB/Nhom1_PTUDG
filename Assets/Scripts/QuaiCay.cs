using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuaiCay : MonoBehaviour, DamebyPlayer
{
    [SerializeField] protected Image hpBar;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected float detectRadius = 4f;
    [SerializeField] protected GameObject posCircle;
    [SerializeField] protected LayerMask checkLayer;
    public float timeToAttack = 3f;
    public bool isFlipped = false;
    protected Animator animator;
    protected float currentHp;
    protected Vector2 startPoint;
    protected Rigidbody2D rb;
    public GameObject bulletPrefabs;
    public Transform firePos;
    public float speedDanThuong = 5;
    Player player;
    bool isdie;
    protected virtual void Start()
    {
        isdie = false;
        player = FindAnyObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        startPoint = transform.position;
        UpdateHpBar();
    }

    protected virtual void Update()
    {
        Attack();   
        LookAtPlayer();
        // CheckEnemy();
    }
    private bool canAttack = true;

    private void Attack()
    {
        if (canAttack)
        {
            StartCoroutine(CheckPlayer());
        }
    }

    IEnumerator CheckPlayer()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(posCircle.transform.position, detectRadius, checkLayer);

        if (playerCollider.Length > 0)
        {
            canAttack = false;
            animator.SetTrigger("isattack");
            yield return new WaitForSeconds(timeToAttack); 
            canAttack = true;
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
        animator.SetTrigger("ishit");
        StartCoroutine(DamageFlash());
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0 && isdie==false)
        {
            Die();
        }
    }

    private void Die()
    {
        isdie = true;
        animator.SetTrigger("isdie");
        Destroy(gameObject, 1f);
        GameManager.Instance.KillEnemy();
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
    public void LookAtPlayer()
    {
        if (player == null) return;

        Vector3 scale = transform.localScale;

        if (transform.position.x > player.transform.position.x)
        {
            // Quay trái
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            // Quay phải
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
    public void BanDanThuong()
    {
        if(player != null)
        {
            Vector3 directisonToPlayer = player.transform.position - firePos.position;
            directisonToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePos.position, Quaternion.identity);
            Bullet enemyBullet = bullet.GetComponent<Bullet>();
            if (enemyBullet != null)
            {
                enemyBullet.SetMoveDirection(directisonToPlayer * speedDanThuong);
            }
        }
    }
}

