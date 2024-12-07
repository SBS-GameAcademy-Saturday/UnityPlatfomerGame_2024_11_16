using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    /// <summary>
    /// Float 파라미터 이름
    /// </summary>
    public string floatName;
    /// <summary>
    /// State 변할때 값을 변경할 건지에 대한 여부
    /// </summary>
    public bool updateOnStateEnter, updateOnStateExit;
    /// <summary>
    /// StateMachine 변할때 값을 변경할 건지에 대한 여부
    /// </summary>
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    /// <summary>
    /// Enter, Exit에 따라 설정할 값 Value
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
