using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFighter
{
    //스탯, 데미지 입기

    public int hp = 100;
    private int guardStamina = 5;

    public static Player CurrentPlayer;
    private GreatSword greatSword;
    private CharacterController controller;

    private void Awake()
    {
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
        hp -= combatEvent.Damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
    }

    public void AttackStart()
    {
        greatSword.GetComponent<Collider>().enabled = true;
    }

    public void AttackEnd()
    {
        greatSword.GetComponent<Collider>().enabled = false;
    }
}