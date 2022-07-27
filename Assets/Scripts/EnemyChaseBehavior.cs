using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChaseBehavior : EnemyBehavior
{
    protected AIPath pathfinder;
    protected AIDestinationSetter setter;



    protected override void Awake()
    {
        base.Awake();

        pathfinder = GetComponent<AIPath>();
        setter = GetComponent<AIDestinationSetter>();
    }

    protected virtual void Update()
    {
        if (pathfinder.velocity.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (pathfinder.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if (currentHealth <= 0) player.OnPlayerInvulnerable -= OnPlayerInvulnerable;
    }

    public override void Spawn(Vector2 position)
    {
        base.Spawn(position);

        pathfinder.maxSpeed = currentMoveSpeed;
        setter.target = player.Invulnerable ? this.transform : player.transform;
        animator.SetBool("moving", true);

        player.OnPlayerInvulnerable += OnPlayerInvulnerable;
    }

    protected virtual void OnPlayerInvulnerable(bool invulnerable)
    {
        setter.target = invulnerable ? this.transform : GameController.GetPlayer().transform;
    }
}