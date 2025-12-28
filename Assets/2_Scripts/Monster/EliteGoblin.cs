using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGoblin : GoblinBase
{
   public GameObject particle;
   
    protected override void OffParticle()
    {
        particle.SetActive(false);
    }
    private void OnEnable()
    {
        particle.SetActive(true);
    }
}