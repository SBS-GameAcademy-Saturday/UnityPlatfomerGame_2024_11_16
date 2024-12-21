using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 데미지
    public int attackDamage = 10;
    // Projectile 이동 속도
    public Vector2 moveSpeed = new Vector2(6, 0);
    // 넉백 Vector
    public Vector2 knockback = Vector2.zero;

    Rigidbody2D _rb;
    private void Awake()
    {
        // RigidBody를 가져온다.
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // RigidBody의 속력을 설정한다.
        float value = transform.localScale.x > 0 ? 1 : -1;
        _rb.velocity = new Vector2(moveSpeed.x * value, moveSpeed.y);

        // 5초동안 살아있으면 파괴한다.
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            Vector2 deliverdKnockback = transform.localScale.x > 0 ?
                knockback : new Vector2(-knockback.x, knockback.y);

            // 데미지를 입히면 현재 Prohjectile은 파괴한다.
            bool gothit = damagable.GetHit(attackDamage, deliverdKnockback);
            if (gothit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
