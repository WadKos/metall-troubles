using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletTransform;
    public bool canFire;
    private float timer;
    [SerializeField] public float timeBetweenFiring;
    private GameObject player;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (mousePos.x < player.transform.position.x)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            bulletTransform.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
            bulletTransform.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
 
        if(Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

        if (Input.GetMouseButton(0))
        {
            bulletTransform.GetComponent<Animator>().Play("Rifle_shoot");
        }
        else
        {
            bulletTransform.GetComponent<Animator>().Play("Rifle_idle");
        }
    }
}
