using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    // 사라지는 효과 지속 시간
    public float fadeTime = 0.5f;
    // 사라지는 효과 지연 시간
    public float fadeDelay = 0.0f;

    // 사라지는 효과 지속 시간 확인 변수
    private float timerElapsed = 0;

    // 사라지는 효과 지연 시간 확인 변수
    private float fadeDelayElapsed = 0;

    // 오브젝트이 Sprite Renderer Component
    SpriteRenderer spriteRenderer;

    // 삭제할 오브젝트
    GameObject objToRemove;

    // 초기 컬러
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
        // 지연 로직
        if(fadeDelay > fadeDelayElapsed)
        {
            // 지연 시간을 Time.Deletaime만큼 줄인다.
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
