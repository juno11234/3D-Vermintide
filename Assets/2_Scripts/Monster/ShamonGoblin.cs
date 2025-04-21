using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShamonGoblin : GoblinBase
{
    
    public GameObject fireBallPrefabs;
    public Transform firePoint;
    protected new void Start()
    {
        base.Start(); // GoblinBase 초기화
        CombatSystem.Instance.RegisterMonster(this);
    }

    public void Fire()
    {
        var fire=Instantiate(fireBallPrefabs,firePoint.position,Quaternion.identity);
    }
}