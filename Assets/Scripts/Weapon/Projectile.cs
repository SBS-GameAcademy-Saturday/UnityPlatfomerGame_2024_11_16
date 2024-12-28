using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Projectile : MonoBehaviour
{
    // ������
    public int attackDamage = 10;
    // Projectile �̵� �ӵ�
    public Vector2 moveSpeed = new Vector2(6, 0);
    // �˹� Vector
    public Vector2 knockback = Vector2.zero;

    // ��ü�� ���� �ð�
    public float lifeTime = 5f;

    // ��ü�� Ÿ�̸�
    private float timer = 0;

    Rigidbody2D _rb;
    private void Awake()
    {
        // RigidBody�� �����´�.
        _rb = GetComponent<Rigidbody2D>();
    }

    public void StartMove()
    {
        // RigidBody�� �ӷ��� �����Ѵ�.
        float value = transform.localScale.x > 0 ? 1 : -1;
        _rb.velocity = new Vector2(moveSpeed.x * value, moveSpeed.y);
    }

    private void OnEnable()
    {
        // ��ü�� �ٽ� Ȱ��ȭ �Ǹ� timer�� 0���� �ʱ�ȭ�Ѵ�.
        timer = 0;
    }

    private void Update()
    {
        // Ÿ�̸Ӹ� ������.
        timer += Time.deltaTime;
        // Ÿ�̸Ӱ� lifeTime���� ũ�� ��ü�� ����
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

            // �������� ������ ���� Prohjectile�� �ı��Ѵ�.
            bool gothit = damagable.GetHit(attackDamage, deliverdKnockback);
            if (gothit)
            {
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }
}
