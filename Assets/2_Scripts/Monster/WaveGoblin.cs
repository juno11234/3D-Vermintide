using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WaveGoblin : GoblinBase
{
    protected new void Start()
    {
        base.Start(); // GoblinBase 초기화
        CombatSystem.Instance.RegisterMonster(this);
    }
}