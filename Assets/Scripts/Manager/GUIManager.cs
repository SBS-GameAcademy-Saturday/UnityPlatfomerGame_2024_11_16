using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GUIManager : MonoBehaviour
{
    /// <summary>
    /// 캐릭터가 데미지를 입었을 때 호출할 델리게이트
    /// </summary>
    public static UnityAction<Vector3, int> characterDamaged;
    /// <summary>
    /// 캐릭터가 체력을 회복했을 때 호출할 델리게이트
    /// </summary>
    public static UnityAction<Vector3, int> characterHealed;

    [SerializeField] GameObject DamageTextPrefab;
    [SerializeField] GameObject HealTextPrefab;

    private Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        characterDamaged += OnCharacterDamaged;
        characterHealed += OnCharacterHealed;
    }


    private void OnDisable()
    {
        characterDamaged -= OnCharacterDamaged;
        characterHealed -= OnCharacterHealed;
    }

    private void OnCharacterDamaged(Vector3 position, int damage)
    {
        // 캐릭터의 월드 좌표를 스크린 좌표로 변환한다.
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(position);

        // 스크린 좌표를 기준으로 DamageTextPrefab을 생성한다.
        GameObject obj = Instantiate(DamageTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform);
        
        TextMeshProUGUI Text = obj.GetComponent<TextMeshProUGUI>();
        Text.text = damage.ToString();
    }

    private void OnCharacterHealed(Vector3 position, int heal)
    {
        // 캐릭터의 월드 좌표를 스크린 좌표로 변환한다.
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(position);

        // 스크린 좌표를 기준으로 HealTextPrefab을 생성한다.
        GameObject obj = Instantiate(HealTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform);

        TextMeshProUGUI Text = obj.GetComponent<TextMeshProUGUI>();
        Text.text = heal.ToString();
    }

}
