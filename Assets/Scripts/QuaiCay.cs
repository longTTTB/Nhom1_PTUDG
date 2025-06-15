using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuaiCay : MonoBehaviour
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
    protected bool movingRight = false;
    public GameObject bulletPrefabs;
    public Transform firePos;
    public float speedDanThuong = 5;
    Player player;

    protected virtual void Start()
    {
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
     public void Shoot()
    {

    }
    // protected virtual void CheckEnemy()
    // {
    //     Collider2D[] enemies = Physics2D.OverlapCircleAll(posCircle.transform.position, detectRadius, checkLayer);
    //     if (enemies.Length > 0)
    //     {
    //         animator.SetBool("isattack", true);
    //     }
    //     else
    //     {
    //         animator.SetBool("isattack", false);
    //     }
    // }

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
        animator.SetTrigger("ishit");
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("isdie");
        Destroy(gameObject, 1f);
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
    public void BanDanThuong()
    {
        if(player != null)
        {
            Vector3 directisonToPlayer = player.transform.position - firePos.position;
            directisonToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePos.position, Quaternion.identity);
            Bullet enemyBullet = bullet.AddComponent<Bullet>();
            enemyBullet.SetMoveDirection(directisonToPlayer * speedDanThuong);
        }
    }
}

