using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private bool vertical = true;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float changeTime = 1.0f;
    [SerializeField] private ParticleSystem smokeEffect;

#pragma warning disable CS0108 // 成員隱藏所繼承的成員; 遺漏 new 關鍵字
    private Rigidbody2D rigidbody2D;
#pragma warning restore CS0108 // 成員隱藏所繼承的成員; 遺漏 new 關鍵字
    private float Timer;
    private int direction = 1;

    private Animator animator;
    private bool broken = true;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!broken) return;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        Timer -= Time.deltaTime;
        if(Timer < 0)
        {
            direction = -direction;
            Timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broken) return;

        Vector2 position = rigidbody2D.position;

        if(vertical)
        {
            position.y += speed * Time.deltaTime * direction;
        }
        else
        {
            position.x += speed * Time.deltaTime * direction;
        }

        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if(playerController != null)
        {
            playerController.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("BeFriend");
        smokeEffect.Stop();
    }
}
