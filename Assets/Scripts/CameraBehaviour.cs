using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -100);
    }
}
