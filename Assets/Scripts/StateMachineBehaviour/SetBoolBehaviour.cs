using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetBoolBehaviour : StateMachineBehaviour
{
    /// <summary>
    /// Bool ������ Ÿ���� �Ķ���� Name
    /// </summary>
    public string boolName;
    /// <summary>
    /// State���� ������ �Ұ���
    /// </summary>
    public bool updateOnState;
    
    /// <summary>
    /// State Machine���� ������ �� ����
    /// </summary>
    public bool updateOnStateMachine;

    /// <summary>
    /// Enter,Exit�϶� ������ ��
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
