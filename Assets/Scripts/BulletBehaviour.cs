using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    private Vector3 mousePosition;
    private float lifeTime = 3f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction;
        if (Mathf.Sqrt(Mathf.Pow((player.transform.position.x - mousePosition.x), 2) + 
            Mathf.Pow((player.transform.position.y- mousePosition.y), 2)) < 1.75f) 
        {
            direction = -(mousePosition - transform.position);
        }
        else
        {
            direction = mousePosition - transform.position;
        }
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject, lifeTime);
        Debug.Log(Mathf.Sqrt(Mathf.Pow((player.transform.position.x - mousePosition.x), 2) + Mathf.Pow((player.transform.position.y - mousePosition.y), 2)));
    }

}
