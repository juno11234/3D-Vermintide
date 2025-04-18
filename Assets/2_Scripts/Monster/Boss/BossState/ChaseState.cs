using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ChaseState : BossState
{
    private static readonly int SPEED = Animator.StringToHash("Speed");
    private int[] bossAttacks;
    private Animator animator;
    private Boss boss;
    private Player player;
    NavMeshAgent agent;

    [SerializeField]
    private float attackRange = 4f;

    public override StateName Name => StateName.ChaseState;

    public override void Initialize(Boss boss)
    {
        this.boss = boss;
        bossAttacks = new[] { Boss.JUMPATTACK, Boss.FOOTATTACK };
        animator = boss.animator;
        agent = boss.GetComponent<NavMeshAgent>();
        player=Player.CurrentPlayer;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, boss.transform.position);

        if (distance < attackRange)
        {
            agent.ResetPath();
            animator.SetFloat(SPEED, 0f);
            int nextAttackTrigger = Random.Range(0, bossAttacks.Length);
            int stateValue = nextAttackTrigger + 1;
            animator.SetTrigger(bossAttacks[nextAttackTrigger]);
            boss.ChageState((StateName)stateValue);
        }
        else
        {
            animator.SetFloat(SPEED, 1f);
            agent.SetDestination(player.transform.position);
        }
    }

    public override void Enter()
    {
        //Debug.Log("Chase Enter");
    }

    public override void Exit()
    {
    }
}