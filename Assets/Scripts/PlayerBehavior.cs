using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : CharacterBehavior
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private List<Weapon> weapons;
    
    private WeaponType currentWeapon;
    private Transform crosshair;
    private float currentAttackCooldown;
    private bool invulnerable; public bool Invulnerable { get => invulnerable; }
    private List<SpriteRenderer> renderers;
    private bool canControl = true;



    public delegate void PlayerInvulnerable(bool invulnerable);
    public event PlayerInvulnerable OnPlayerInvulnerable;
    


    protected override void Awake()
    {
        base.Awake();

        renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

        crosshair = GameObject.Find("Crosshair").transform;
        currentWeapon = WeaponType.SWORD;
    }

    private void Update()
    {
        currentAttackCooldown += Time.deltaTime;

        if (!canControl) return;

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput != Vector2.zero) { Move(moveInput); }
        else { animator.SetBool("moving", false); }

        Vector3 mousePos = Input.mousePosition; mousePos.z += 10; mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mouseDir = mousePos - transform.position;
        if (Input.GetButtonDown("Fire") && currentAttackCooldown >= 1 / attackSpeed) { Attack(mousePos); }
        if (mouseDir.magnitude > weapons[(int)currentWeapon].range)
        {
            crosshair.position = (Vector2)transform.position + (mouseDir.normalized * weapons[(int)currentWeapon].range);
        }
        else
        {
            crosshair.position = mousePos;
        }
    }

    private void Attack(Vector2 clickPosition)
    {
        currentAttackCooldown = 0f;

        if (currentWeapon != WeaponType.NULL)
        {
            weapons[(int)currentWeapon].Attack(clickPosition);
        }
    }

    public override void TakeDamage(float amount)
    {
        if (invulnerable) return;

        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            this.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(false);
            UIController.UpdateLives(0);
            GameController.GameOver();
        }
        else
        {
            transform.position = Vector3.zero;
            UIController.UpdateLives(Mathf.Floor(currentHealth));
            StartCoroutine(InvulnerabilityRoutine());
        }
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        invulnerable = true;
        OnPlayerInvulnerable?.Invoke(true);

        for (int i = 0; i < 15; i++)
        {
            foreach (SpriteRenderer sr in renderers) sr.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            foreach (SpriteRenderer sr in renderers) sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        invulnerable = false;
        OnPlayerInvulnerable?.Invoke(false);
    }

    public void EnterDoor(Vector2 doorPosition)
    {
        StartCoroutine(EnterDoorRoutine(doorPosition));
    }

    private IEnumerator EnterDoorRoutine(Vector2 doorPosition)
    {
        canControl = false;
        
        Stop();
        yield return new WaitForSeconds(1f);

        MoveSpeed = 1f;
        GetComponent<Collider2D>().enabled = false;
        Vector2 selfPosition = transform.position;
        Vector2 direction = (doorPosition - selfPosition).normalized;
        float time = 0f;
        while (time < 2f)
        {
            time += Time.deltaTime;

            Move(direction);

            yield return null;
        }

        GameController.GameOver(); // change to GameController.NextLevel()
    }
}