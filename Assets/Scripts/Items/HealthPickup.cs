using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // ü�� ȸ���� ������ ��
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    // ����� ����� Source
    AudioSource _pickupSource;

    private void Awake()
    {
        _pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            // ü�� ȸ�� ����
            if (damagable.Heal(healthRestore))
            {
                if (_pickupSource)
                {
                    // ���� ��ü�� �ı� �ǹǷ� �Ʒ��� �ڵ�� ������� �ʴ´�.
                    // _pickupSource.Play();

                    AudioSource.PlayClipAtPoint(_pickupSource.clip, gameObject.transform.position, _pickupSource.volume);
                }

                // ü���� ȸ�� �������� �ڱ� �ڽ��� �ı��մϴ�.
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
