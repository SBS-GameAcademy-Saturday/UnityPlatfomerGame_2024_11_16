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

    // 게임 종료 버튼 클릭시 처리
    public void OnExitGame(InputAction.CallbackContext context)
    {
        Debug.Log(context.started);
        // 게임 종료 버튼을 눌렀다.
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if UNITY_EDITOR             
            // 유니티 에디터
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            // 모바일, PC에서는 아래의 코드로 게임을 종료
            Application.Quit();
#endif
        }
    }

}
