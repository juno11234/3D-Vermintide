using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShamanGoblin : GoblinBase
{
    public GameObject fireBallPrefabs;
    public Transform firePoint;
    public GameObject particle;

    protected override void OffParticle()
    {
        particle.SetActive(false);
       
    }
    private void OnEnable()
    {
        particle.SetActive(true);
    }
    public override void WeaponCollOn()
    {
        Instantiate(fireBallPrefabs, firePoint.position, Quaternion.identity);
    }
    public override void WeaponCollOff()
    {
    }
}