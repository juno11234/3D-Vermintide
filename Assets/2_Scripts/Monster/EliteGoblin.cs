using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGoblin : GoblinBase
{
    ParticleSystem particle;

    protected new void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
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

    protected override void Die()
    {
        base.Start();
        particle.Stop();
        particle.Clear();
    }
}