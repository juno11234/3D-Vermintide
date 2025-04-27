using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GreatSword : WeaponBase
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    private static readonly int BLOCK = Animator.StringToHash("Block");
    private static readonly int SKILL = Animator.StringToHash("Skill");

    //무기 데미지, 가드, 스킬등을 담당
    [SerializeField]
    private float staminaCooldown = 3f;

    [SerializeField]
    private float staminaRegenTime = 1f;

    private Collider collider;
    private int damage = 10;

    public int maxGuardStamina = 4;
    public float maxSkillGage = 90f;

    public int currentStamina { get; private set; }
    private float regenTimer;
    public float currentSkillGage;
    private bool inCooldown = false;
    public bool isGuarding = false;
    private Animator animator;
    

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        currentStamina = maxGuardStamina;
        currentSkillGage = maxSkillGage;

        collider = GetComponent<Collider>();
        collider.enabled = false;

       

        CombatSystem.Instance.Events.OnEnemyDieEvents += SkillGagePlus;
    }

    private void Update()
    {
        GuardStaminaRegen();
        SkillGageUpdate();
    }

    //공격로직
    public override void Attack()
    {
        animator.ResetTrigger(ATTACK);
        animator.SetTrigger(ATTACK);
    }

    public override void OnAttackStart()
    {
        collider.enabled = true;
    }

    public override void OnAttackEnd()
    {
        collider.enabled = false;
    }

    //스킬로직
    public override void Skill()
    {
        if (CanSkill() == false) return;
        currentSkillGage = 0;
        animator.SetTrigger(SKILL);
    }

    public override bool CanSkill()
    {
        return currentSkillGage >= maxSkillGage;
    }

    private void SkillGageUpdate()
    {
        currentSkillGage += Time.deltaTime;
        currentSkillGage = Mathf.Min(currentSkillGage, maxSkillGage);
    }

    private void SkillGagePlus(EnemyDieEvents enemyDieEvents)
    {
        currentSkillGage += 0.5f;
    }

    //가드로직
    public override void Guard(bool isGuard)
    {
        isGuarding = isGuard;
        animator.SetBool(BLOCK, isGuard);
    }

    public override bool CanGuard()
    {
        return currentStamina > 0 && inCooldown == false;
    }
    
    public bool TryGuard(int damage)
    {
        if (CanGuard() == false || isGuarding == false) return false;

        int staminaConsume = damage / 5;
        currentStamina -= staminaConsume;

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            animator.SetBool(BLOCK, false);
            StartCoroutine(GuardCoolCorutine());
        }

        return true;
    }

    IEnumerator GuardCoolCorutine()
    {
        inCooldown = true;
        yield return new WaitForSeconds(staminaCooldown);
        inCooldown = false;
    }

    private void GuardStaminaRegen()
    {
        if (inCooldown || currentStamina >= maxGuardStamina) return;

        regenTimer += Time.deltaTime;
        if (regenTimer >= staminaRegenTime)
        {
            currentStamina++;
            regenTimer = 0f;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
        {
            //Debug.Log("공격!");
            var monster = CombatSystem.Instance.GetMonsterOrNull(other);
            if (monster != null)
            {
                CombatEvents combatEvents = new CombatEvents();
                combatEvents.Sender = Player.CurrentPlayer;
                combatEvents.Receiver = monster;
                combatEvents.Damage = damage;
                combatEvents.HitPosition = other.ClosestPoint(transform.position);
                combatEvents.Collider = other;

                CombatSystem.Instance.AddInGameEvent(combatEvents);
            }
        }
    }
}