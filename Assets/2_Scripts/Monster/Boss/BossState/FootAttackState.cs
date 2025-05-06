using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FootAttackState : BossState
{
    [FormerlySerializedAs("footAttackSFX")]
    [SerializeField]
    private SFXData AttackSFX;
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
        
        if (currentState.normalizedTime > 0.7)
            footAttack.gameObject.SetActive(false);
        else if (currentState.normalizedTime > 0.4)
        {
            footAttack.gameObject.SetActive(true);
            SFXManager.Instance.PlayNoDuplicate(AttackSFX);
        }


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