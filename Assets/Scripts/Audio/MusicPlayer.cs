using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // 게임을 시작할 때 1번만 재생할 사운드
    [SerializeField] AudioSource _introSource;

    // Intro 사운드 재생 후에 반복적으로 재생할 사운드
    [SerializeField] AudioSource _loopSource;

    void Start()
    {
        // introSource는 바로 재생
        _introSource.Play();
        // loopSource는 (오디오 설정에서 현재 오디오가 관리되는 다른 현재 시간값 + introSource의 사운드 재생 길이 시간) 후에 재생
        _loopSource.PlayScheduled(AudioSettings.dspTime + _introSource.clip.length);
    }
}
