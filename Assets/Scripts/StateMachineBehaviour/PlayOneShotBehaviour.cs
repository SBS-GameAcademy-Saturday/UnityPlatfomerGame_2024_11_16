using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    // 재생할 오디오 클립
    public AudioClip soundToPlay;

    // 오디오 볼륨
    public float volume = 1f;

    // 오디오 클립을 재생할 타이밍 변수
    public bool playOnEnter = true;
    public bool playOnExit = false;
    public bool playAfterDelay = false;

    // playAfterDelay가 켜져있을 떄 지연시킬 시간 초 값
    public float playDelay = 0.25f;

    // 애니메이션이 플레이되기 시작한 후에 경과 시간
    private float timeSinceEntered = 0;
    // 오디오 지연 재생이 됬는지 판단하는 변수
    private bool hasDelayedSoundPlayed = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 애니메이션 시작시 사운드를 재생할 건지 여부
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }

        timeSinceEntered = 0;
        hasDelayedSoundPlayed = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 애니메이션 중에 지연된 재생을 할건지 혹은 이미 재생된 적이 있는지 확인
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            // 경과 시간이 지연 시간보다 크면 재생
            if (timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                // 재생 후에는 재생된 적이 있는지 확인 변수를 true로 변동한다.
                hasDelayedSoundPlayed = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 애니메이션 종료시 사운드를 재생할 건지 여부
        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }
}
