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
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        QuaiCay quaicay = collision.GetComponent<QuaiCay>();
        if (enemy != null && !alreadyHit.Contains(collision))
        {
            enemy.TakeDamage(damage);
            alreadyHit.Add(collision);
        }
        else if (quaicay != null && !alreadyHit.Contains(collision))
        {
            quaicay.TakeDamage(damage);
            alreadyHit.Add(collision);
        }
    }
}

