using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] private GameEvent OnMoving;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    //TODO: (need fix) BUILD時會出現BlendTreeWorkSpace is NULL的問題
    //private Animator _ani;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        //_ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        FlipSprite();
        //UpdateAnimationParam();
    }

    //private void UpdateAnimationParam()
    //{
    //    _ani.SetFloat("LookAtX", lookDirection.x);
    //    _ani.SetFloat("LookAtY", lookDirection.y);
    //    _ani.SetFloat("MovementX", moveDirection.x);
    //    _ani.SetFloat("MovementY", moveDirection.y);
    //    if (moveDirection.x > 0f || moveDirection.y > 0f)
    //        _ani.SetBool("IsMoving", true);
    //    else
    //        _ani.SetBool("IsMoving", false);

    //}

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
        if (moveDirection != Vector2.zero)
            lookDirection = moveDirection;

        OnMoving?.Invoke();
    }

    private void FlipSprite()
    {
        if (lookDirection.x < 0f) _sr.flipX = true;
        else if (lookDirection.x > 0f) _sr.flipX = false;
    }
}
