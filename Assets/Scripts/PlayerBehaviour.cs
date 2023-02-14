using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float _speed;

    private float hInput;
    private float vInput;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        hInput = Input.GetAxis("Horizontal") * _speed;
        vInput = Input.GetAxis("Vertical") * _speed;
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.Play("Player_walk");
        } else
        {
            anim.Play("Player_idle");
        }
        this.transform.Translate(Vector2.right * Time.fixedDeltaTime * hInput);
        this.transform.Translate(Vector2.up * Time.fixedDeltaTime * vInput);
    }

    public void takeDamage(int damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            death();
        }
    }

    private void death()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            takeDamage(collision.gameObject.GetComponent<BulletBehaviour>().getDamage());
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Animator>().Play("Fireball_destroy");
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
