using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationStrings
{
    // 정적 변수로 선언해서 
    // 어떤 객체든 편하게 접근이 가능 하도록 설계
    public static string IsMovingParameterName = "IsMoving";
    public static string IsRunningParameterName = "IsRunning";
    public static string JumpParameterName = "Jump";
    public static string yVelocityParameterName = "yVelocity";
    
    // 해쉬값 통해서 제어 
    public static int IsMoving = Animator.StringToHash(IsMovingParameterName);
    public static int IsRunning = Animator.StringToHash(IsRunningParameterName);
    public static int Jump = Animator.StringToHash(JumpParameterName);
    public static int yVelocity = Animator.StringToHash(yVelocityParameterName);
}