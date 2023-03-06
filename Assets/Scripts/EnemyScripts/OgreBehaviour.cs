using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBehaviour : EnemyBehaviour
{
    private float maxSpeed;
    [SerializeField] private float speedBoost = 2f;


    private float attackTimer = 0;


    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float attackDistance;
    [SerializeField] private float attackReload;

    [SerializeField] private float dashMaxTime;
    private float dashTimer;
    private bool isDash;

    private bool onceDamage;
    [SerializeField] private bool canDamage;
    [SerializeField] private float damageDistance;

    [SerializeField] private int damage = 25;


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

        if (isDash && dashTimer < dashMaxTime && distanceToPlayer >= minDistance)
        {
            dashTimer += Time.fixedDeltaTime;
            DamageToPlayer();
        }
        else
        {
            speed = maxSpeed;
        }


        if (distanceToPlayer <= attackDistance && attackTimer >= attackReload)
        {
            Attack();
        }

        if (attackTimer < attackReload)
        {
            attackTimer += Time.fixedDeltaTime;
        }

        canDamage = distanceToPlayer <= damageDistance;

    }

    private void Attack()
    {
        onceDamage = true;
        dashTimer = 0;
        isDash = true;
        speed = maxSpeed * speedBoost;
        attackTimer = 0;
        anim.SetTrigger("OgreAttack");
        Dash();

    }

    private void DamageToPlayer()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);

        if (canDamage && onceDamage)
        {
            hitPlayer.GetComponent<PlayerBehaviour>().TakeDamage(damage);
            Debug.Log("Hit");
            onceDamage = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void Dash()
    {
        if (dashTimer <= dashMaxTime)
        {
            speed = maxSpeed * speedBoost;
            dashTimer += Time.deltaTime;
        }
        else
        {
            speed = maxSpeed;
            dashTimer = 0;
        }
    }
}
    



