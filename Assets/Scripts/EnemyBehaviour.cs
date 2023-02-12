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

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //СМЭРТЬ от пули + уничтожение пули
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(collision.gameObject.GetComponent<BulletBehaviour>().getDamage());
            Destroy(collision.gameObject);
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
        Destroy(gameObject);
    }


    private void FixedUpdate()
    {
        distanceToPlayer = Mathf.Sqrt(Mathf.Pow(playerPosition.position.x - this.gameObject.transform.position.x, 2f) +
            Mathf.Pow(playerPosition.position.y - this.gameObject.transform.position.y, 2f));

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
