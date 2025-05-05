using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GreatSword : WeaponBase
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    private static readonly int BLOCK = Animator.StringToHash("Block");
    private static readonly int SKILL = Animator.StringToHash("Skill");

    //무기 데미지, 가드, 스킬등을 담당
    [SerializeField]
    private ParticleSystem skillparticle;

    [SerializeField]
    private WeaponCool cool;

    [SerializeField]
    private List<SFXData> hit;

    private Collider collider;
    private int damage = 10;

    public int maxGuardStamina = 4;
    public float maxSkillGage = 90f;

    public bool isGuarding = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

        collider = GetComponent<Collider>();
        collider.enabled = false;
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
        skillparticle.Play();
        cool.currentSkillGage = 0;
        animator.SetTrigger(SKILL);
    }

    public override bool CanSkill()
    {
        return cool.currentSkillGage >= maxSkillGage;
    }

    //가드로직
    public override void Guard(bool isGuard)
    {
        isGuarding = isGuard;
        animator.SetBool(BLOCK, isGuard);
    }

    public override bool CanGuard()
    {
        return cool.currentStamina > 0 && cool.inCooldown == false;
    }

    public bool TryGuard(int damage)
    {
        if (CanGuard() == false || isGuarding == false) return false;

        int staminaConsume = damage / 5;
        cool.currentStamina -= staminaConsume;

        if (cool.currentStamina <= 0)
        {
            cool.currentStamina = 0;
            animator.SetBool(BLOCK, false);
            cool.inCooldown = true;
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
        {
            int index = Random.Range(0, hit.Count);
            SFXManager.Instance.Play(hit[index]);
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