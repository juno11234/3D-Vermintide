using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShamonGoblin : GoblinBase
{
    public GameObject fireBallPrefabs;
    public Transform firePoint;
    public GameObject particle;

    protected new void Start()
    {
        base.Start(); // GoblinBase 초기화
        CombatSystem.Instance.RegisterMonster(this);
    }

    public void Fire()
    {
        Instantiate(fireBallPrefabs, firePoint.position, Quaternion.identity);
    }

    public override void TakeDamage(CombatEvents combatEvent)
    {
        if (isDead) return;
        goblinStat.hp -= combatEvent.Damage;

        if (goblinStat.hp <= 0)
        {
            Die();
            particle.SetActive(false);
        }
        else animator.SetTrigger(HIT);
    }

    private void OnEnable()
    {
        particle.SetActive(true);
    }
}