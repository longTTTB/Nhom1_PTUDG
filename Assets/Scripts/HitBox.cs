using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private List<Collider2D> alreadyHit = new List<Collider2D>();
    public float damage = 10;
    private Collider2D hitboxCollider;

    void Awake()
    {
        hitboxCollider = GetComponent<Collider2D>();
        if (hitboxCollider == null)
        {
            Debug.LogError("chua cai collider");
        }
    }
    void OnEnable()
    {
        alreadyHit.Clear();

        // Kiểm tra tất cả các kẻ địch hiện đang ở trong vùng trigger
        if (hitboxCollider != null)
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            hitboxCollider.OverlapCollider(filter, colliders);

            foreach (Collider2D collision in colliders)
            {
                if (collision == null) continue;

                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null && !alreadyHit.Contains(collision))
                {
                    enemy.TakeDamage(damage);
                    alreadyHit.Add(collision);
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && !alreadyHit.Contains(collision))
        {
            enemy.TakeDamage(damage);
            alreadyHit.Add(collision);
        }
    }
}

