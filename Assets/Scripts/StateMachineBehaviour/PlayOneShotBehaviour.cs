using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    // ����� ����� Ŭ��
    public AudioClip soundToPlay;

    // ����� ����
    public float volume = 1f;

    // ����� Ŭ���� ����� Ÿ�̹� ����
    public bool playOnEnter = true;
    public bool playOnExit = false;
    public bool playAfterDelay = false;

    // playAfterDelay�� �������� �� ������ų �ð� �� ��
    public float playDelay = 0.25f;

    // �ִϸ��̼��� �÷��̵Ǳ� ������ �Ŀ� ��� �ð�
    private float timeSinceEntered = 0;
    // ����� ���� ����� ����� �Ǵ��ϴ� ����
    private bool hasDelayedSoundPlayed = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ִϸ��̼� ���۽� ���带 ����� ���� ����
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }

        timeSinceEntered = 0;
        hasDelayedSoundPlayed = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ִϸ��̼� �߿� ������ ����� �Ұ��� Ȥ�� �̹� ����� ���� �ִ��� Ȯ��
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            // ��� �ð��� ���� �ð����� ũ�� ���
            if (timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                // ��� �Ŀ��� ����� ���� �ִ��� Ȯ�� ������ true�� �����Ѵ�.
                hasDelayedSoundPlayed = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ִϸ��̼� ����� ���带 ����� ���� ����
        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }
}
