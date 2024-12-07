using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    // ������� ȿ�� ���� �ð�
    public float fadeTime = 0.5f;
    // ������� ȿ�� ���� �ð�
    public float fadeDelay = 0.0f;

    // ������� ȿ�� ���� �ð� Ȯ�� ����
    private float timerElapsed = 0;

    // ������� ȿ�� ���� �ð� Ȯ�� ����
    private float fadeDelayElapsed = 0;

    // ������Ʈ�� Sprite Renderer Component
    SpriteRenderer spriteRenderer;

    // ������ ������Ʈ
    GameObject objToRemove;

    // �ʱ� �÷�
    Color startColor;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timerElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� ����
        if(fadeDelay > fadeDelayElapsed)
        {
            // ���� �ð��� Time.Deletaime��ŭ ���δ�.
            fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            timerElapsed += Time.deltaTime;

            // 1 - 0.2 / 1 => 0.8 / 1.0 => 4/5
            float newAlpha = startColor.a * (1 - timerElapsed / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            if(timerElapsed > fadeTime)
            {
                Destroy(objToRemove);
            }
        }

    }
}
