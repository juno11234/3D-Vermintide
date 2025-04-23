using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IFighter
{
    public static Boss CurrentBoss;
    public static readonly int JUMPATTACK = Animator.StringToHash("JumpAttack");
    public static readonly int FOOTATTACK = Animator.StringToHash("FootAttack");


    public enum Parts
    {
        Unknow,
        Body,
        LeftLeg,
        RightLeg,
        LeftArm,
        RightArm,
    }

    public class BossStat
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
    }


    public Collider MainCollider => BossParts.bodyColl;

    public GameObject GameObject => gameObject;

    public BossStat Stat { get; private set; }

    public BossState CurrentState { get; private set; }
    public BossParts BossParts;
    public Animator animator;
    public bool isDead { get; private set; }

    private Dictionary<BossState.StateName, BossState> stateDictionary =
        new Dictionary<BossState.StateName, BossState>();

    void Awake()
    {
        isDead = false;
        CurrentBoss = this;
        Stat = new BossStat();

        Stat.HP = 500;
        Stat.MaxHP = 500;
    }

    void Start()
    {
        BossParts.Initialize();
        var bossPartsArray = BossParts.BossNodeArray;

        for (int i = 0; i < bossPartsArray.Length; i++)
            CombatSystem.Instance.RegisterBossMonster(bossPartsArray[i].Collider, this);

        BossState[] states = GetComponentsInChildren<BossState>();
        //보스스테이트 상속 스크립트 전부 모음

        for (int i = 0; i < states.Length; i++)
        {
            //딕셔너리에 이름을 키값으로 넣고 초기화
            stateDictionary.Add(states[i].Name, states[i]);
            states[i].Initialize(this);
            states[i].gameObject.SetActive(false);
        }

        ChageState(BossState.StateName.ChaseState);
    }

    public void ChageState(BossState.StateName enterState)
    {
        if (CurrentState != null)
        {
            //널이 아니라면 이전 상태 저장하고 비활성화
            BossState prev = CurrentState;
            prev.Exit();
            prev.gameObject.SetActive(false);
        }

        BossState targetState = stateDictionary[enterState];
        CurrentState = targetState;
        targetState.gameObject.SetActive(true);
        targetState.Enter();
    }

    public void TakeDamage(CombatEvents combatEvent)
    {
        if (isDead) return;
        Stat.HP -= combatEvent.Damage;
        //Debug.Log(Stat.HP);
        if (Stat.HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Dead");
        isDead = true;
        
    }
}