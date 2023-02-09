using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    private Rigidbody2D _rb;
    private Vector3 mousePosition;
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.velocity = this.transform.forward * _bulletSpeed;
        Destroy(this, 3f);
    }

    private void Update()
    {
        mousePosition = Input.mousePosition;
    }
}
