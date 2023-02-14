using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Animator anim;
    private Camera mainCam;
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    private Vector3 mainPosition;
    private float lifeTime = 3f;
    private GameObject player;
    [SerializeField] private int damage;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        if (this.gameObject.tag == "Bullet")
        {
            mainPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        } 
        else
        {
            mainPosition = player.transform.position;
        }
        Vector3 direction;
        Vector3 rotation;
        if (Mathf.Sqrt(Mathf.Pow((player.transform.position.x - mainPosition.x), 2) + 
            Mathf.Pow((player.transform.position.y- mainPosition.y), 2)) < 0.5f && this.gameObject.CompareTag("Bullet")) 
        {
            direction = -(mainPosition - transform.position);
            rotation = -(transform.position - mainPosition);
        }
        else
        {
            direction = mainPosition - transform.position;
            rotation = transform.position - mainPosition;
        }
        
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        //Debug.Log(Mathf.Sqrt(Mathf.Pow((player.transform.position.x - mousePosition.x), 2) + Mathf.Pow((player.transform.position.y - mousePosition.y), 2)));
    }
    private void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0)
        {
            if (gameObject.CompareTag("Bullet"))
            {
                anim.Play("Bullet_destroy");
                Destroy(gameObject, 0.2f);
            }
            else if (gameObject.CompareTag("EnemyBullet"))
            {
                anim.Play("Fireball_destroy");
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((this.gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("Enemy")) || 
            (this.gameObject.CompareTag("EnemyBullet") && collision.gameObject.CompareTag("Player")))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public int getDamage()
    {
        return this.damage;
    }
}
