using UnityEngine;

public class HucChieu : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 50f;

    private Vector3 direction;

    public void SetDirectionAndFlip(Vector3 dir, bool flip)
    {
        direction = dir.normalized;

        // Nếu đang flip → lật lại sprite
        if (flip)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // lật ngang
            transform.localScale = localScale;
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamebyPlayer enemy = collision.GetComponent<DamebyPlayer>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
