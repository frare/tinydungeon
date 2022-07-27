using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { NULL, SWORD }

public class Weapon : MonoBehaviour
{
    public float range;

    protected Transform character;

    private Animator animator;



    protected virtual void Awake()
    {
        character = GetComponentInParent<CharacterBehavior>().transform;
        animator = GetComponent<Animator>();
    }

    public virtual void Attack(Vector2 clickPosition)
    {
        animator.SetTrigger("attack");
    }
}