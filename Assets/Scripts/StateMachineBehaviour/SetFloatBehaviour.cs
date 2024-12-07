using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    /// <summary>
    /// Float �Ķ���� �̸�
    /// </summary>
    public string floatName;
    /// <summary>
    /// State ���Ҷ� ���� ������ ������ ���� ����
    /// </summary>
    public bool updateOnStateEnter, updateOnStateExit;
    /// <summary>
    /// StateMachine ���Ҷ� ���� ������ ������ ���� ����
    /// </summary>
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    /// <summary>
    /// Enter, Exit�� ���� ������ �� Value
    /// </summary>
    public float valueOnEnter, valueOnExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }   
    }
}
