using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GUIManager : MonoBehaviour
{
    /// <summary>
    /// ĳ���Ͱ� �������� �Ծ��� �� ȣ���� ��������Ʈ
    /// </summary>
    public static UnityAction<Vector3, int> characterDamaged;
    /// <summary>
    /// ĳ���Ͱ� ü���� ȸ������ �� ȣ���� ��������Ʈ
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
        // ĳ������ ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ�Ѵ�.
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(position);

        // ��ũ�� ��ǥ�� �������� DamageTextPrefab�� �����Ѵ�.
        GameObject obj = Instantiate(DamageTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform);
        
        TextMeshProUGUI Text = obj.GetComponent<TextMeshProUGUI>();
        Text.text = damage.ToString();
    }

    private void OnCharacterHealed(Vector3 position, int heal)
    {
        // ĳ������ ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ�Ѵ�.
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(position);

        // ��ũ�� ��ǥ�� �������� HealTextPrefab�� �����Ѵ�.
        GameObject obj = Instantiate(HealTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform);

        TextMeshProUGUI Text = obj.GetComponent<TextMeshProUGUI>();
        Text.text = heal.ToString();
    }

}
