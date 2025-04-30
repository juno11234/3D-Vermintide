using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GoblinBase : MonoBehaviour, IFighter, IObjectPoolItem
{
    protected static readonly int SPEED = Animator.StringToHash("Speed");
    protected static readonly int JUMP = Animator.StringToHash("Jump");
    protected static readonly int ATTACK = Animator.StringToHash("Attack");
    protected static readonly int HIT = Animator.StringToHash("Hit");
    private static readonly int DEAD = Animator.StringToHash("Dead");

    [System.Serializable]
    public class GoblinStat
    {
        public int hp;
        public int maxHp;
        public float range;
    }

    public Collider MainCollider => collider;
    public string Key { get; set; }
    public GameObject GameObject => gameObject;
    public BloodControll.BloodType bloodType => BloodControll.BloodType.Monster;
    protected Player player;

    [SerializeField]
    protected GoblinStat goblinStat;

    protected Animator animator;
    protected Collider collider;
    protected NavMeshAgent agent;
    protected bool isJumping = false;
    protected AnimatorStateInfo currentState;
    protected GoblinWepon goblinWepon;
    protected bool isDead = false;


    protected void Start()
    {
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = Player.CurrentPlayer;
        goblinWepon = GetComponentInChildren<GoblinWepon>();
    }


    private void OnEnable()
    {
        goblinStat.hp = goblinStat.maxHp;
    }

    protected void Update()
    {
        if (isDead) return;
        UpdateCustom();
    }

    protected virtual void UpdateCustom()
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

    protected void Chase()
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

    protected IEnumerator Jump()
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

    protected void Attack()
    {
        if (currentState.IsName("Jump")) return;


        animator.SetFloat(SPEED, 0f);
        agent.isStopped = true;

        Vector3 direction = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        animator.SetTrigger(ATTACK);
    }

    public void WeponCollOn()
    {
        goblinWepon.GetComponent<Collider>().enabled = true;
    }

    public void WeponCollOff()
    {
        goblinWepon.GetComponent<Collider>().enabled = false;
    }

    public virtual void TakeDamage(CombatEvents combatEvent)
    {
        if (isDead) return;
        goblinStat.hp -= combatEvent.Damage;

        if (goblinStat.hp <= 0)
        {
            Die();
        }
        else animator.SetTrigger(HIT);
    }

    protected virtual void Die()
    {
        EnemyDieEvents e = new EnemyDieEvents();
        CombatSystem.Instance.AddInGameEvent(e);
        animator.SetTrigger(DEAD);
        isDead = true;
        collider.enabled = false;
        agent.enabled = false;

        StartCoroutine(DieDelay());
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(3f);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        isDead = false;
        collider.enabled = true;
        agent.enabled = true;
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
}