using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : BossState
{
    public JumpAttack jumpAttack;
    private Animator animator;
    private Boss boss;

    [SerializeField]
    private SFXData attack;

    public override StateName Name => StateName.JumpAttackState;

    public override void Initialize(Boss boss)
    {
        animator=boss.animator;
        this.boss = boss;
        
        jumpAttack.gameObject.SetActive(false);
    }

    void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        if(currentState.IsName(animatorStateName)==false)return;
        if (currentState.normalizedTime > 0.5)
        {
            SFXManager.Instance.PlayNoDuplicate(attack);
        }
        if (currentState.normalizedTime > exitTime)
        {
            boss.ChageState(StateName.ChaseState);
        }
    }

    public override void Enter()
    {
        jumpAttack.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        jumpAttack.gameObject.SetActive(false);
        animator.ResetTrigger("JumpAttack");
    }
}