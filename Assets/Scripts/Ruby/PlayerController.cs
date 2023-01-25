using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;

    [SerializeField] private float speed;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;

    [SerializeField] private float timeInvincible = 2.0f;
    [SerializeField] private bool isInvincible;
    [SerializeField] private float invincibleTimer;

    private Vector2 lookDirection = new Vector2(1, 0);

    Animator animator;

    public GameObject projectilePrefab;

    private AudioSource audioSource;


    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get { return currentHealth; } }


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        speed = 5.0f;
    }

    void Update()
    {
        MoveInput();
        UpdateAnimation();
        UpdateInvincible();

        LaunchInput();
        RayCasting();
    }

    private void UpdateInvincible()
    {
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void LaunchInput()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    private void RayCasting()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f,
                lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if(npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }
    }

    private void MoveInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();
        }
    }

    private void Move()
    {
        Vector2 position = rigidbody2d.position;
        position.x += horizontal * speed * Time.deltaTime;
        position.y += vertical * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("MoveX", horizontal);
    }

    public void ChangeHealth(int health)
    {
        if(health < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        UIHealthBar.instance.SetValue(CurrentHealth / (float) MaxHealth);
    }

    private void Launch()
    {
        GameObject projectileGameObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up, Quaternion.identity);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
