using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGoblin : GoblinBase
{
    //정해진 구역 패트롤 플레이어 발각시 추격 사정거리내면 공격

    [SerializeField]
    private Transform[] patrolPoints;
    
    bool isChasing = false;

    protected override void UpdateCustom()
    {
        if (isChasing) base.UpdateCustom();
        else Patrol();
    }

    void Patrol()
    {
    }

    void StartChasing()
    {
        isChasing = true;
    }
}