using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 공격력
    public int attackDamage = 10;
    // 넉백 Vector(항상 양수로 Vector값을 설정해야 합니다.)
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        // Damagable Component가 있다면
        if (damagable)
        {
            // 캐릭터가 오른쪽을 보고 있으면 오른쪽으로 KnockBack이 설정되고
            // 캐릭터가 왼쪽을 보고 있으면 왼쪽으로 KnockBack이 설정됩니다.
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ?
                knockback : new Vector2(-knockback.x, knockback.y);

            damagable.GetHit(attackDamage, deliveredKnockback);
        }
    }
}
