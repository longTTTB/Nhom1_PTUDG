using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDirection == Vector3.zero) return;
        transform.position += moveDirection * Time.deltaTime;
    }
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
