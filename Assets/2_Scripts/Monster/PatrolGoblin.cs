using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGoblin : GoblinBase
{
    //정해진 구역 패트롤 플레이어 발각시 추격 사정거리내면 공격
    [SerializeField]
    private float viewAngle = 120;

    [SerializeField]
    private float viewDistance = 10f;

    [SerializeField]
    private Transform[] patrolPoints;

    bool isChasing = false;
    private Vector3 direction;
    int destinationIndex = 0;

    protected new void Start()
    {
        base.Start(); // GoblinBase 초기화
        CombatSystem.Instance.RegisterMonster(this);
    }

    protected override void UpdateCustom()
    {
        if (isChasing) base.UpdateCustom();

        else
        {
            Patrol();
            WatchPlayer();
        }
    }

    void Patrol()
    {
        agent.SetDestination(patrolPoints[destinationIndex].position);
        animator.SetFloat(SPEED, 0.5f);
        agent.speed = 2f;
        if (agent.pathPending == false && agent.remainingDistance < 0.2f)
        {
            if (destinationIndex < patrolPoints.Length - 1)
            {
                destinationIndex++;
            }
            else
            {
                destinationIndex = 0;
            }
        }
    }

    void WatchPlayer()
    {
        if (goblinStat.hp < goblinStat.maxHp)
        {
            ChaseStart();
            return;
        }

        Vector3 direction = (Player.CurrentPlayer.transform.position - transform.position);
        float distance = direction.magnitude;
        direction.Normalize();
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle > viewAngle * 0.5f || distance > viewDistance) return;

        Physics.Raycast(transform.position + Vector3.up, direction, out RaycastHit hit, viewDistance);
        if (hit.collider == Player.CurrentPlayer.MainCollider)
        {
            ChaseStart();
        }
    }

    void ChaseStart()
    {
        agent.ResetPath();
        isChasing = true;
        animator.SetFloat(SPEED, 1);
        agent.speed = 6f;
    }
    protected override void Die()
    {
        EnemyDieEvents e = new EnemyDieEvents();
        CombatSystem.Instance.AddInGameEvent(e);
        animator.SetTrigger("Dead");
        isDead = true;
        collider.enabled = false;
        agent.enabled = false;
    }
}