using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : EnemyBehaviour
{
    [SerializeField] private int damage = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Death();
            playerPosition.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        }
    }
    protected override void Death()
    {
        base.Death();
        canMove = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        anim.Play("Light_death");
        Destroy(gameObject, 0.5f);
    }
}
