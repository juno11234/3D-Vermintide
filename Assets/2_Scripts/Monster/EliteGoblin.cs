using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGoblin : GoblinBase
{
    protected new void Start()
    {
        base.Start(); // GoblinBase 초기화
        CombatSystem.Instance.RegisterMonster(this);
    }

    public override void TakeDamage(CombatEvents combatEvent)
    {
        if (isDead) return;
        goblinStat.hp -= combatEvent.Damage;

        if (goblinStat.hp <= 0)
        {
            Die();
        }
    }
    
}