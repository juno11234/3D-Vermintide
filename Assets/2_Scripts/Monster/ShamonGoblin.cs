using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShamonGoblin : GoblinBase
{
    
    public GameObject fireBallPrefabs;
    public Transform firePoint;
    ParticleSystem particle;
    protected new void Start()
    {
        particle =GetComponentInChildren<ParticleSystem>();
        base.Start(); // GoblinBase 초기화
        CombatSystem.Instance.RegisterMonster(this);
    }

    public void Fire()
    {
        Instantiate(fireBallPrefabs,firePoint.position,Quaternion.identity);
    }
    protected override void Die()
    {
        base.Start();
        particle.Stop();
        particle.Clear();
    }
}