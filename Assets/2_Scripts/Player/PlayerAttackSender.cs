using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSender : StateMachineBehaviour
{
    [Range(0f, 1f)]
    public float startNormalizedTime = 0f;

    private bool passStartNormalizedTime;

    [Range(0f, 1f)]
    public float endNormalizedTime = 0f;
    
    private bool passEndNormalizedTime;
    
    private Player player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        passStartNormalizedTime = false;
        passEndNormalizedTime = false;
        player = animator.gameObject.GetComponentInParent<Player>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (passStartNormalizedTime==false && startNormalizedTime < stateInfo.normalizedTime)
        {
            player.AttackStart();
            passStartNormalizedTime = true;
        }

        if (passEndNormalizedTime==false && endNormalizedTime < stateInfo.normalizedTime)
        {
            player.AttackEnd();
            passEndNormalizedTime = true;
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