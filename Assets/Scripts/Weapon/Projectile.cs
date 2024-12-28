using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Projectile : MonoBehaviour
{
    // 데미지
    public int attackDamage = 10;
    // Projectile 이동 속도
    public Vector2 moveSpeed = new Vector2(6, 0);
    // 넉백 Vector
    public Vector2 knockback = Vector2.zero;

    // 객체의 수명 시간
    public float lifeTime = 5f;

    // 객체의 타이머
    private float timer = 0;

    Rigidbody2D _rb;
    private void Awake()
    {
        // RigidBody를 가져온다.
        _rb = GetComponent<Rigidbody2D>();
    }

    public void StartMove()
    {
        // RigidBody의 속력을 설정한다.
        float value = transform.localScale.x > 0 ? 1 : -1;
        _rb.velocity = new Vector2(moveSpeed.x * value, moveSpeed.y);
    }

    private void OnEnable()
    {
        // 객체가 다시 활성화 되면 timer를 0으로 초기화한다.
        timer = 0;
    }

    private void Update()
    {
        // 타이머를 돌린다.
        timer += Time.deltaTime;
        // 타이머가 lifeTime보다 크면 객체를 끈다
        if (timer > lifeTime)
            this.gameObject.SetActive(false);
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
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }
}
