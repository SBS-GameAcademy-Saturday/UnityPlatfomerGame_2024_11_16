using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // ������
    public int attackDamage = 10;
    // Projectile �̵� �ӵ�
    public Vector2 moveSpeed = new Vector2(6, 0);
    // �˹� Vector
    public Vector2 knockback = Vector2.zero;

    Rigidbody2D _rb;
    private void Awake()
    {
        // RigidBody�� �����´�.
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // RigidBody�� �ӷ��� �����Ѵ�.
        float value = transform.localScale.x > 0 ? 1 : -1;
        _rb.velocity = new Vector2(moveSpeed.x * value, moveSpeed.y);

        // 5�ʵ��� ��������� �ı��Ѵ�.
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            Vector2 deliverdKnockback = transform.localScale.x > 0 ?
                knockback : new Vector2(-knockback.x, knockback.y);

            // �������� ������ ���� Prohjectile�� �ı��Ѵ�.
            bool gothit = damagable.GetHit(attackDamage, deliverdKnockback);
            if (gothit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
