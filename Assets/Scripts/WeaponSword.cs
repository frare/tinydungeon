using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack(Vector2 clickPosition)
    {
        base.Attack(clickPosition);
        
        Vector2 projectileSpawnPos = clickPosition;
        Vector2 projectileSpawnDir = (clickPosition - (Vector2)character.position);
        if (projectileSpawnDir.magnitude > range)
        {
            projectileSpawnPos = (Vector2)character.position + (projectileSpawnDir.normalized * range);
        }

        foreach (Collider2D collider in Physics2D.OverlapCircleAll(projectileSpawnPos, 0.75f, 1 << 10))
        {
            if (!collider.isTrigger) continue;

            collider.GetComponentInParent<EnemyChaseBehavior>().TakeDamage(1);
        }

        ProjectileController.SpawnSwordProjectile(projectileSpawnPos);
    }
}