using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    // ���� ���� ��ư Ŭ���� ó��
    public void OnExitGame(InputAction.CallbackContext context)
    {
        Debug.Log(context.started);
        // ���� ���� ��ư�� ������.
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if UNITY_EDITOR             
            // ����Ƽ ������
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            // �����, PC������ �Ʒ��� �ڵ�� ������ ����
            Application.Quit();
#endif
        }
    }

}
