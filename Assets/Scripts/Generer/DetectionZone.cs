using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    // 현재 나와 Trigger 되어있는 오브젝트 목록이 비어있을 때 호출하는 이벤트입니다.
    public UnityEvent onNoCollidersRemainEvent = new UnityEvent();

    // 현재 나와 Trigger 되어있는 오브젝트 목록
    public List<Collider2D> detectionColiders = new List<Collider2D>();

    Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Trigger 되었을 때 detectionColiders에 해당 Trigger된 Colider를 추가한다.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectionColiders.Add(collision);
    }

    /// <summary>
    /// Trigger를 빠져나왔을 때 detectionColiders에 해당 Colider를 제거한다.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectionColiders.Remove(collision);
        //  detectionColiders의 Count가 0 이하일 때
        if (detectionColiders.Count <= 0)
        {
            // onNoCollidersRemainEvent를 호출한다.
            onNoCollidersRemainEvent.Invoke();
        }
    }

}
