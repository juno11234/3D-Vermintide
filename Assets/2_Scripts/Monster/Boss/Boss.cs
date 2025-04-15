using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    //플레이어 추격 사정거리내에 들어오면 공격1,2중 랜덤 시전
    //hp가 절반 이하되면 새로운 패턴 추가
    //가드후 패턴파훼시 그로기 아니면 대쉬공격
    //공격시 플레이어가 뒤로 조금 밀려남
    //대쉬 공격은 밀려나는 거리증가 
    public enum BossState
    {
        Moving,
        Attacking
    }

    private static readonly int SPEED = Animator.StringToHash("Speed");

    private static readonly int FOOT_ATTACK = Animator.StringToHash("FootAttack");
    private static readonly int JUMP_ATTACK = Animator.StringToHash("JumpAttack");


    [SerializeField]
    private Player player;

    [SerializeField]
    private float attackDistance = 10f;

    private BossState currentState;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = BossState.Moving;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        switch (currentState)
        {
            case BossState.Moving:
                if (distance <= attackDistance)
                {
                    Debug.Log("공격상태전환");
                    EnterAttackState();
                }

                else
                    EnterMoveState();

                break;
            case BossState.Attacking:
                if (distance > attackDistance)
                    EnterMoveState();
                else
                    EnterAttackState();
                break;
        }
    }

    private void EnterMoveState()
    {
        currentState = BossState.Moving;
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        animator.SetFloat(SPEED, 1f);
    }

    private void EnterAttackState()
    {
        currentState = BossState.Attacking;
        animator.SetFloat(SPEED, 0f);
        agent.isStopped = true;
        agent.ResetPath();

        Attack();
    }

    private void Attack()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            animator.SetTrigger(FOOT_ATTACK);
        }
        else
        {
            animator.SetTrigger(JUMP_ATTACK);
        }
    }
}