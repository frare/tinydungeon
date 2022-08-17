using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer mainRenderer;

    protected float currentHealth;
    private float currentMoveSpeed;
    protected Rigidbody2D rb;
    protected Animator animator;
    private List<Material> materials;

    protected float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;
            currentMoveSpeed = moveSpeed;  
            animator.speed = currentMoveSpeed / 2;
        }
    }

    protected float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            currentMoveSpeed = value;
            animator.speed = currentMoveSpeed / 2;
        }
    }



    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        materials = new List<Material>();
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) 
            foreach (Material mat in sprite.materials) 
                materials.Add(mat);

        currentHealth = health;
        CurrentMoveSpeed = moveSpeed;
    }

    protected virtual void Move(Vector2 direction)
    {
        rb.MovePosition(rb.position + (direction.normalized * currentMoveSpeed * Time.fixedDeltaTime));
        if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("moving", true);
    }

    protected virtual void LateUpdate()
    {
        mainRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

    protected virtual void Stop()
    {
        animator.SetBool("moving", false);
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Debug.Log($"{gameObject.name} is defeated!");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"{gameObject.name} took {amount} damage!");
            StartCoroutine("TakeDamageRoutine");
        }
    }

    private IEnumerator TakeDamageRoutine()
    {
        float lerpTime = 0.0f;
        while (lerpTime < 0.2f)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / 0.2f;

            foreach (Material material in materials) material.SetFloat("_FlashAmount", 1f - perc);
            yield return null;
        }
    }
}