using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] GameObject _bullet;
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

}
