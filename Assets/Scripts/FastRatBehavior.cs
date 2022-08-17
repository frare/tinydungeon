using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRatBehavior : EnemyChaseBehavior
{
    [SerializeField] private float slowMultiplier;

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
        CurrentMoveSpeed = MoveSpeed / slowMultiplier;
        pathfinder.maxSpeed = CurrentMoveSpeed;

        rb.AddForce((this.transform.position - player.transform.position).normalized * 5, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);

        pathfinder.canMove = true;

        yield return new WaitForSeconds(2f);

        MoveSpeed = MoveSpeed;
        pathfinder.maxSpeed = CurrentMoveSpeed;
    }

    protected override void Reset()
    {
        base.Reset();

        pathfinder.canMove = true;
        pathfinder.maxSpeed = CurrentMoveSpeed;
    }
}