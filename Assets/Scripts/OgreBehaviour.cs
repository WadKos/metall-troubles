using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBehaviour : EnemyBehaviour
{
    private float maxSpeed;
    [SerializeField] private float dashMaxTime = 0.5f;
    [SerializeField] private float dashTime = 0f;
    [SerializeField] private float dashTimer = 0f;
    [SerializeField] private float dashReload = 5f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private float dashDistance = 3f;

    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private float distanceAttack = 0.1f;
    private float time;
    [SerializeField] private int damage = 25;


    private bool CanAttack()
    {
        if (distanceToPlayer <= minDistance + distanceAttack && time > attackInterval)
        {
            return true;
        }
        return false;
    }
    protected override void Start()
    {
        maxSpeed = speed;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Death()
    {
        canMove = false;
        //Destroy(transform.GetChild(0).gameObject);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        anim.Play("Ogre_death");
        Destroy(gameObject, 0.3f);
    }

    private void FixedUpdate()
    {
        BasicMove();

        if (CanAttack())
        {
            Attack();
        }
        
        if (time < attackInterval)
        {
            time += Time.fixedDeltaTime;
        }

        if (!canDash)
        {
            speed = maxSpeed;
            dashTimer += Time.deltaTime;
            if (dashTimer > dashReload)
            {
                canDash = true;
                dashTimer = 0;
            }
        }

        if (dashDistance >= distanceToPlayer && canDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (dashTime < dashMaxTime)
        {
            dashTime += Time.deltaTime;
            speed = maxSpeed * 5;
        }
        else
        {
            speed = maxSpeed;
            canDash = false;
            dashTime = 0;
        }
        
    }


    protected void Attack()
    {
        anim.Play("ogre_attack");
        playerPosition.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        time = 0;
        
    }
}
