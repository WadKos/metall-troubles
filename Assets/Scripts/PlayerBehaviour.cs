using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float _speed;

    private float hInput;
    private float vInput;
    private Vector3 mousePosition;

    private void Update()
    {
        hInput = Input.GetAxis("Horizontal") * _speed;
        vInput = Input.GetAxis("Vertical") * _speed;
    }
    private void FixedUpdate()
    {
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
            Destroy(collision.gameObject);
        }
    }
}
