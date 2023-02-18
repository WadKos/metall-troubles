using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBehaviour : EnemyBehaviour
{
    protected override void Death()
    {
        canMove = false;
        Destroy(transform.GetChild(0).gameObject);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        anim.Play("Mage_death");
        Destroy(gameObject, 0.5f);
    }
    private void FixedUpdate()
    {
        BasicMove();
    }
}
