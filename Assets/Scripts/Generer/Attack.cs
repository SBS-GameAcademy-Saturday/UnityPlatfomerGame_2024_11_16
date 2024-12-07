using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // ���ݷ�
    public int attackDamage = 10;
    // �˹� Vector(�׻� ����� Vector���� �����ؾ� �մϴ�.)
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        // Damagable Component�� �ִٸ�
        if (damagable)
        {
            // ĳ���Ͱ� �������� ���� ������ ���������� KnockBack�� �����ǰ�
            // ĳ���Ͱ� ������ ���� ������ �������� KnockBack�� �����˴ϴ�.
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ?
                knockback : new Vector2(-knockback.x, knockback.y);

            damagable.GetHit(attackDamage, deliveredKnockback);
        }
    }
}
