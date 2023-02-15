using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 0.5f;
    private Transform playerPosition;
    [SerializeField] private float minDistance = 5f;
    private float distanceToPlayer;
    private float distanceInterval = 0.5f;
    private Animator anim;
    private Rigidbody2D rb;
    private bool canMove = true;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //СМЭРТЬ от пули + уничтожение пули
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(collision.gameObject.GetComponent<BulletBehaviour>().getDamage());
            collision.gameObject.GetComponent<Animator>().Play("Bullet_destroy");
            Destroy(collision.gameObject, 0.05f);
        }
    }

    //Нанесение урона
    public void takeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            death();
        }
    }

    //СМЭРТЬ
    private void death()
    {
        canMove = false;
        DestroyImmediate(transform.GetChild(0).gameObject);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        anim.Play("Mage_death");
        Destroy(gameObject, 0.5f);
    }


    private void FixedUpdate()
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

}
