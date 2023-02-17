using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBehaviour : EnemyBehaviour
{

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
    }

    protected void Attack()
    {
        anim.Play("ogre_attack");
        playerPosition.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        time = 0;
        
    }
}
