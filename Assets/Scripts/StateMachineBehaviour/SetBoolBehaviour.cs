using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetBoolBehaviour : StateMachineBehaviour
{
    /// <summary>
    /// Bool 데이터 타입의 파라미터 Name
    /// </summary>
    public string boolName;
    /// <summary>
    /// State에서 설정을 할건지
    /// </summary>
    public bool updateOnState;
    
    /// <summary>
    /// State Machine에서 설정을 할 건지
    /// </summary>
    public bool updateOnStateMachine;

    /// <summary>
    /// Enter,Exit일때 설정할 값
    /// </summary>
    public bool valueOnEnter, valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
