using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAttackState : BossState
{
    public FootAttack footAttack;
    private Animator animator;
    private Boss boss;

    public override StateName Name => StateName.FootAttackState;

    public override void Initialize(Boss boss)
    {
        animator = boss.animator;
        this.boss = boss;

        footAttack.gameObject.SetActive(false);
    }

    void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(animatorStateName) == false) return;
        
        if (currentState.normalizedTime > 0.75)
            footAttack.gameObject.SetActive(false);
        else if (currentState.normalizedTime > 0.4)
            footAttack.gameObject.SetActive(true);


        if (currentState.normalizedTime > exitTime)
        {
            boss.ChageState(StateName.ChaseState);
        }
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }
}