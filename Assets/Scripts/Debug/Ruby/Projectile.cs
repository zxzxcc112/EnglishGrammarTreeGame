using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
#pragma warning disable CS0108 // �������é��~�Ӫ�����; ��| new ����r
    Rigidbody2D rigidbody2D;
#pragma warning restore CS0108 // �������é��~�Ӫ�����; ��| new ����r

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if(enemy != null)
        {
            enemy.Fix();
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if(transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 derection, float force)
    {
        rigidbody2D.AddForce(derection * force);
    }
}
