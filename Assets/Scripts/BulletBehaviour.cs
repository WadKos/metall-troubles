using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    private Vector3 mainPosition;
    private float lifeTime = 3f;
    private GameObject player;
    [SerializeField] private int damage;

    void Start()
    {
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
        if (Mathf.Sqrt(Mathf.Pow((player.transform.position.x - mainPosition.x), 2) + 
            Mathf.Pow((player.transform.position.y- mainPosition.y), 2)) < 1.75f && this.gameObject.tag == "Bullet") 
        {
            direction = -(mainPosition - transform.position);
        }
        else
        {
            direction = mainPosition - transform.position;
        }
        Vector3 rotation = transform.position - mainPosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject, lifeTime);
        //Debug.Log(Mathf.Sqrt(Mathf.Pow((player.transform.position.x - mousePosition.x), 2) + Mathf.Pow((player.transform.position.y - mousePosition.y), 2)));
    }



    public int getDamage()
    {
        return this.damage;
    }
}
