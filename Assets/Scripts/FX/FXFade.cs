using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXFade : MonoBehaviour
{
    private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.enabled = true;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        animator.enabled = false;
    }
}