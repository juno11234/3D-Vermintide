using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    public enum StateName
    {
        ChaseState,
        JumpAttackState,
        FootAttackState,
        GuardState,
        DashAttackState,
        StunState,
    }
    
    public abstract StateName Name { get; }

    public string animatorStateName;
    public float exitTime;
    
    public abstract void Initialize(Boss boss);
    public abstract void Enter();
    public abstract void Exit();
}
