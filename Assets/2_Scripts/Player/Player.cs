using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IFighter
{
    //스탯, 데미지 입기
    [System.Serializable]
    public class PlayerStat
    {
        public int hp = 100;
        public int maxHp = 100;
    }

    public static Player CurrentPlayer;
    private GreatSword greatSword;
    private CharacterController controller;
    private bool isDead = false;
    public PlayerStat stat;

    private void Awake()
    {
        stat = new PlayerStat();
        stat.hp = stat.maxHp;
        CurrentPlayer = this;
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        greatSword = GetComponentInChildren<GreatSword>();
    }

    public Collider MainCollider => controller;
    public GameObject GameObject => gameObject;

    public void TakeDamage(CombatEvents combatEvent)
    {
        if (greatSword.TryGuard(combatEvent.Damage) || isDead)
        {
            return;
        }

        stat.hp -= combatEvent.Damage;
        if (stat.hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }

    public void AttackStart()
    {
        greatSword.GetComponent<Collider>().enabled = true;
    }

    public void AttackEnd()
    {
        greatSword.GetComponent<Collider>().enabled = false;
    }

    public void SkillStart()
    {
        greatSword.skill.gameObject.SetActive(true);
    }

    public void SkillEnd()
    {
        greatSword.skill.gameObject.SetActive(false);
    }
}