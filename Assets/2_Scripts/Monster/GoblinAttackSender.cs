using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackSender : StateMachineBehaviour
{
    [Range(0f, 1f)]
    public float startNormalizedTime = 0f;

    private bool passStartNormalizedTime;


    [Range(0f, 1f)]
    public float endNormalizedTime = 0f;

    private bool passEndNormalizedTime;

    [Range(0f, 1f)]
    public float secondStartNormalizedTime = 0f;

    private bool secondPassStartNormalizedTime;

    [Range(0f, 1f)]
    public float secondEndNormalizedTime = 0f;

    private bool secondPassEndNormalizedTime;

    private GoblinBase goblin;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        passStartNormalizedTime = false;
        passEndNormalizedTime = false;
        secondPassStartNormalizedTime = false;
        secondPassEndNormalizedTime = false;
        goblin = animator.GetComponent<GoblinBase>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (passStartNormalizedTime == false && startNormalizedTime < stateInfo.normalizedTime)
        {
            goblin.WeponCollOn();
            passStartNormalizedTime = true;
        }

        if (passEndNormalizedTime == false && endNormalizedTime < stateInfo.normalizedTime)
        {
            goblin.WeponCollOff();
            passEndNormalizedTime = true;
        }

        if (secondPassStartNormalizedTime == false && secondStartNormalizedTime < stateInfo.normalizedTime)
        {
            goblin.WeponCollOn();
        }

        if (secondPassEndNormalizedTime == false && secondEndNormalizedTime < stateInfo.normalizedTime)
        {
            goblin.WeponCollOff();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}