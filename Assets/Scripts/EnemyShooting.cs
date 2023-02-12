using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private Vector3 playerPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletTransform;
    public bool canFire;
    private float timer;
    [SerializeField] public float timeBetweenFiring;

    void Update()
    {
        //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        playerPos = GameObject.Find("Player").transform.position;

        Vector3 rotation = playerPos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
 
        if(canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);

        }
    }
}
