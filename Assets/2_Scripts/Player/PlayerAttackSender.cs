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

    [SerializeField]
    private SFXData attack1;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        passStartNormalizedTime = false;
        passEndNormalizedTime = false;
        player = animator.gameObject.GetComponentInParent<Player>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
        if (passStartNormalizedTime==false && startNormalizedTime < stateInfo.normalizedTime)
        {
            SFXManager.Instance.Play(attack1);
            player.AttackStart();
            passStartNormalizedTime = true;
        }

        if (passEndNormalizedTime==false && endNormalizedTime < stateInfo.normalizedTime)
        {
            player.AttackEnd();
            passEndNormalizedTime = true;
        }
    }  
}