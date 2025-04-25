using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController animatorController;
    public RuntimeAnimatorController AnimatorController => animatorController;
    public virtual void Attack()
    {
    }

    public virtual void Skill()
    {
    }

    public virtual void Guard(bool isGuard)
    {
    }

    public virtual void Reload()
    {
    }

    public virtual void OnAttackStart()
    {
    }

    public virtual void OnAttackEnd()
    {
    }

    public virtual bool CanGuard() => false;
    
    public virtual bool CanSkill() => false;
}