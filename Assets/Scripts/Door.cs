using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<Collider2D> doorColliders;
    [SerializeField] private List<SpriteRenderer> renderers;
    [SerializeField] private Transform triggerZone;
    private bool isOpen;



    private void Awake()
    {
        // foreach (SpriteRenderer renderer in renderers) renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
    }

    public void Open()
    {
        animator.SetBool("open", true);
        foreach (Collider2D collider in doorColliders) collider.enabled = false;
        isOpen = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen)
        {
            isOpen = false;
            other.GetComponent<PlayerBehavior>().EnterDoor((Vector2)triggerZone.position + new Vector2(0f, 1f));
        }
    }
}