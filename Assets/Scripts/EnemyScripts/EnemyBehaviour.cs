using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 0.5f;
    protected Transform playerPosition;
    [SerializeField] protected float minDistance = 5f;
    [SerializeField] protected float distanceToPlayer;
    [SerializeField] protected float distanceInterval = 0.5f;
    [SerializeField] protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected bool canMove = true;

    protected virtual void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //СМЭРТЬ от пули + уничтожение пули
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletBehaviour>().getDamage());
            collision.gameObject.GetComponent<Animator>().Play("Bullet_destroy");
            Destroy(collision.gameObject, 0.05f);
        }
    }

    protected void BasicMove()
    {
        if (playerPosition.position.x > this.gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        distanceToPlayer = Mathf.Sqrt(Mathf.Pow(playerPosition.position.x - this.gameObject.transform.position.x, 2f) +
            Mathf.Pow(playerPosition.position.y - this.gameObject.transform.position.y, 2f));

        if (canMove)
        {
            if (distanceToPlayer > minDistance + distanceInterval)
            {
                this.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, playerPosition.position, speed * Time.fixedDeltaTime);
            }
            else if (distanceToPlayer < minDistance - distanceInterval)
            {
                this.transform.position = (Vector2.MoveTowards(this.gameObject.transform.position, playerPosition.position, -speed * Time.fixedDeltaTime));
            }
        }

    }
    //Нанесение урона
    protected void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            Death();
        }
    }

    //СМЭРТЬ
    protected virtual void Death()
    {

    }


    private void FixedUpdate()
    {
        BasicMove();
    }

}
