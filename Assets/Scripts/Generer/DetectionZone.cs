using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    // ���� ���� Trigger �Ǿ��ִ� ������Ʈ ����� ������� �� ȣ���ϴ� �̺�Ʈ�Դϴ�.
    public UnityEvent onNoCollidersRemainEvent = new UnityEvent();

    // ���� ���� Trigger �Ǿ��ִ� ������Ʈ ���
    public List<Collider2D> detectionColiders = new List<Collider2D>();

    Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Trigger �Ǿ��� �� detectionColiders�� �ش� Trigger�� Colider�� �߰��Ѵ�.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectionColiders.Add(collision);
    }

    /// <summary>
    /// Trigger�� ���������� �� detectionColiders�� �ش� Colider�� �����Ѵ�.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectionColiders.Remove(collision);
        //  detectionColiders�� Count�� 0 ������ ��
        if (detectionColiders.Count <= 0)
        {
            // onNoCollidersRemainEvent�� ȣ���Ѵ�.
            onNoCollidersRemainEvent.Invoke();
        }
    }

}
