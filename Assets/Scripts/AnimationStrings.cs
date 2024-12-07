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
    public static string IsGroundedParameterName = "IsGrounded";
    public static string AttackParameterName = "Attack";

    public static string CanMoveParameterName = "CanMove";
    public static string HasTargetParameterName = "HasTarget";
    public static string IsAliveParameterName = "IsAlive";
    public static string HitParameterName = "Hit";
    public static string AttackCoolDownParameterName = "AttackCoolDown";
    public static string LockVelocityParameterName = "LockVelocity";

    // 해쉬값 통해서 제어 
    public static int IsMoving = Animator.StringToHash(IsMovingParameterName);
    public static int IsRunning = Animator.StringToHash(IsRunningParameterName);
    public static int Jump = Animator.StringToHash(JumpParameterName);
    public static int yVelocity = Animator.StringToHash(yVelocityParameterName);
    public static int IsGrounded = Animator.StringToHash(IsGroundedParameterName);
    public static int Attack = Animator.StringToHash(AttackParameterName);

    public static int CanMove = Animator.StringToHash(CanMoveParameterName);
    public static int HasTarget = Animator.StringToHash(HasTargetParameterName);
    public static int IsAlive = Animator.StringToHash(IsAliveParameterName);
    public static int Hit = Animator.StringToHash(HitParameterName);
    public static int AttackCoolDown = Animator.StringToHash(AttackCoolDownParameterName);
    public static int LockVelocity = Animator.StringToHash(LockVelocityParameterName);
}
