using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set : MonoBehaviour
{
    public float damage = 30;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(DelayedDamage(player));
        }
    }

    IEnumerator DelayedDamage(Player target)
    {
        yield return new WaitForSeconds(0.8f);
        target.TakeDamage(damage);
    }
}
