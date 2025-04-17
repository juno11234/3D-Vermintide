using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFighter
{
    //스탯, 데미지 입기
    
    private int hp = 100;
    private int guardStamina = 5;

    public static Player CurrentPlayer;
    private GreatSword greatSword;

    private void Awake()
    {
        CurrentPlayer = this;
    }

    private void Start()
    {
        greatSword = GetComponentInChildren<GreatSword>();
    }

    public Collider MainCollider { get; }
    public GameObject GameObject { get; }

    public void TakeDamage(int damage)
    {
        hp -= damage;
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