using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : CharacterBehavior
{
    [SerializeField] private float collisionDamage = 1;
    protected PlayerBehavior player;



    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if (currentHealth <= 0) EnemyController.OnEnemyDefeated();
    }

    public virtual void Spawn(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);

        Reset();
    }

    protected virtual void Reset()
    {
        player = GameController.GetPlayer();
        currentHealth = health;
        CurrentMoveSpeed = MoveSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponentInParent<CharacterBehavior>().TakeDamage(collisionDamage);
    }
}
