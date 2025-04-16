using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WaveGoblin : MonoBehaviour, IFighter
{
    private static readonly int SPEED = Animator.StringToHash("Speed");
    private static readonly int JUMP = Animator.StringToHash("Jump");
    private static readonly int ATTACK = Animator.StringToHash("Attack");

    //플레이어를 향해 이동 공격사거리 안에 있으면 공격
    //공격 모션시 무기 콜라이더 활성화
    [System.Serializable]
    public class GoblinStat
    {
        public int hp;
        public int maxHp;
        public float range;
    }

    public Collider MainCollider => collider;
    public GameObject GameObject => gameObject;

    
    private Player player;

    [SerializeField]
    GoblinStat goblinStat;

    private Animator animator;
    private Collider collider;
    private NavMeshAgent agent;
    private bool isJumping = false;
    private AnimatorStateInfo currentState;

    void Start()
    {
        collider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = Player.CurrentPlayer;
    }

    private void OnEnable()
    {
        goblinStat.hp = goblinStat.maxHp;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (distance < goblinStat.range)
        {
            Attack();
        }
        else if (distance > goblinStat.range)
        {
            animator.ResetTrigger(ATTACK);
            Chase();
        }
    }

    private void Chase()
    {
        if (currentState.IsName("Attack")) return;
        agent.isStopped = false;
        animator.SetFloat(SPEED, 1f);
        agent.SetDestination(player.transform.position);
        if (agent.isOnOffMeshLink && isJumping == false)
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;
        animator.SetTrigger(JUMP);
        var data = agent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = data.endPos + Vector3.up * 0.1f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;

            Vector3 height = Vector3.up * (Mathf.Sin(t * Mathf.PI) * 1.5f);
            transform.position = Vector3.Lerp(start, end, t) + height;

            yield return null;
        }

        agent.CompleteOffMeshLink();
        isJumping = false;
    }

    private void Attack()
    {
        if (currentState.IsName("Jump")) return;

        animator.SetFloat(SPEED, 0f);
        agent.isStopped = true;

        Vector3 direction = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        animator.SetTrigger(ATTACK);
    }

    public void TakeDamage(int damage)
    {
    }
}