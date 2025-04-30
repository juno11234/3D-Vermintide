using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCool : MonoBehaviour
{
    [SerializeField]
    GreatSword greatSword;

    public float currentSkillGage;
    public float maxSkillGage;

    public int currentStamina;
    public int maxStamina;
    private float regenTimer;
    public bool inCooldown = false;

    public float staminaCooldown = 3f;

    float staminaRegenTime = 1f;

 

    private void Start()
    {
        CombatSystem.Instance.Events.OnEnemyDieEvents += SkillGagePlus;

        currentSkillGage = greatSword.maxSkillGage;
        maxSkillGage = greatSword.maxSkillGage;

        currentStamina = greatSword.maxGuardStamina;
        maxStamina = greatSword.maxGuardStamina;
    }

    void Update()
    {
        SkillGageUpdate();
        GuardStaminaRegen();
        if (inCooldown)
        {
            StartCoroutine(GuardCoolCorutine());
        }
    }

    private void SkillGageUpdate()
    {
        if (currentSkillGage <=maxSkillGage)
        {
            currentSkillGage += Time.deltaTime;
        }
        else
        {
            currentSkillGage = maxSkillGage;
        }
    }

    private void SkillGagePlus(EnemyDieEvents enemyDieEvents)
    {
        currentSkillGage += 0.5f;
    }

    private void GuardStaminaRegen()
    {
        if (inCooldown || currentStamina >=maxStamina) return;

        regenTimer += Time.deltaTime;
        if (regenTimer >= staminaRegenTime)
        {
            currentStamina++;
            regenTimer = 0f;
        }
    }
    IEnumerator GuardCoolCorutine()
    {
        yield return new WaitForSeconds(staminaCooldown);
        inCooldown = false;
    }
}