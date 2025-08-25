using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController animatorController;
    public RuntimeAnimatorController AnimatorController => animatorController;
    public virtual void RMBClick()
    {
    }

    public virtual void Skill()
    {
    }

    public virtual void RightClick(bool isclick)
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