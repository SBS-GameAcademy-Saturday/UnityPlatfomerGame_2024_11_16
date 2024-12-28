using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // ������ ������ �� 1���� ����� ����
    [SerializeField] AudioSource _introSource;

    // Intro ���� ��� �Ŀ� �ݺ������� ����� ����
    [SerializeField] AudioSource _loopSource;

    void Start()
    {
        // introSource�� �ٷ� ���
        _introSource.Play();
        // loopSource�� (����� �������� ���� ������� �����Ǵ� �ٸ� ���� �ð��� + introSource�� ���� ��� ���� �ð�) �Ŀ� ���
        _loopSource.PlayScheduled(AudioSettings.dspTime + _introSource.clip.length);
    }
}
