using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 0.5f;
    private Transform playerPosition;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //СМЭРТЬ от пули + уничтожение пули
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            makeDamage(collision.gameObject.GetComponent<BulletBehaviour>().getDamage());
            Destroy(collision.gameObject);
        }
    }

    //Нанесение урона
    public void makeDamage(int damage)
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
        this.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, playerPosition.position, speed * Time.fixedDeltaTime);
    }

}
