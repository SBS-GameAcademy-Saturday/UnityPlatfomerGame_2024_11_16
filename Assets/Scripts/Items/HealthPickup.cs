using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // 체력 회복을 시켜줄 값
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            // 체력 회복 여부
            if (damagable.Heal(healthRestore))
            {
                // 체력을 회복 시켰으면 자기 자신을 파괴합니다.
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
