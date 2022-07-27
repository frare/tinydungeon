using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRatBehavior : EnemyChaseBehavior
{
    protected Coroutine slowdown;



    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if (currentHealth > 0)
        {
            if (slowdown != null) StopCoroutine(slowdown); 
            slowdown = StartCoroutine(Slowdown());
        }
    }

    protected IEnumerator Slowdown()
    {
        pathfinder.canMove = false;
        currentMoveSpeed = moveSpeed / 3;
        pathfinder.maxSpeed = currentMoveSpeed;
        animator.speed = currentMoveSpeed / 2;

        rb.AddForce((this.transform.position - player.transform.position).normalized * 5, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);

        pathfinder.canMove = true;

        yield return new WaitForSeconds(2f);

        currentMoveSpeed = moveSpeed;
        pathfinder.maxSpeed = currentMoveSpeed;
        animator.speed = currentMoveSpeed / 2;
    }

    protected override void Reset()
    {
        base.Reset();

        pathfinder.canMove = true;
        currentMoveSpeed = moveSpeed;
        pathfinder.maxSpeed = currentMoveSpeed;
        animator.speed = currentMoveSpeed / 2;
    }
}