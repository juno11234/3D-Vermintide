using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GreatSword : MonoBehaviour
{
    //무기 데미지, 가드, 스킬등을 담당

    private Collider collider;
    private int damage = 10;

    [SerializeField]
    private float staminaCooldown = 3f;

    [SerializeField]
    private float staminaRegenTime = 1f;

    [SerializeField]
    private int maxGuardStamina = 4;

    public int currentStamina { get; private set; }
    private float regenTimer;
    private bool inCooldown = false;
    private bool isGuarding = false;
    private Animator animator;
    public GreatSwordSkill skill;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        currentStamina = maxGuardStamina;
        collider = GetComponent<Collider>();
        collider.enabled = false;
        skill = GetComponentInChildren<GreatSwordSkill>();
        skill.gameObject.SetActive(false);
    }

    private void Update()
    {
        GuardStaminaRegen();
    }

    public void GuardState(bool guarding)
    {
        isGuarding = guarding;
    }

    private void GuardStaminaRegen()
    {
        if (inCooldown) return;

        if (currentStamina < maxGuardStamina)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= staminaRegenTime)
            {
                currentStamina++;
                regenTimer = 0f;
            }
        }
    }

    public bool TryGuard(int damage)
    {
        if (inCooldown || isGuarding == false) return false;

        int staminaConsume = damage / 5;
        currentStamina -= staminaConsume;
        Debug.Log(currentStamina);
        if (currentStamina <= 0)
        {
            animator.SetBool("Block", false);
            currentStamina = 0;
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