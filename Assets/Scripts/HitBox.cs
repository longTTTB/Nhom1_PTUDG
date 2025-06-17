using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private List<Collider2D> alreadyHit = new List<Collider2D>();
    public float damage = 10;

    void OnEnable()
    {
        alreadyHit.Clear();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        DamebyPlayer enemy = collision.GetComponent<DamebyPlayer>();
        if (enemy != null && !alreadyHit.Contains(collision))
        {
            enemy.TakeDamage(damage);
            alreadyHit.Add(collision);
        }
    }
}

