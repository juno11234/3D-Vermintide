using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFighter
{
    //스탯, 데미지 입기,스킬 효과 
    private int hp = 100;
    private int guardStamina = 5;

    private static Player CurrentPlayer;
    GreatSword greatSword;

    private void Start()
    {
        CurrentPlayer = this;
        greatSword = GetComponentInChildren<GreatSword>();
    }

    public Collider mainCollider { get; }

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
        greatSword.collider.enabled = true;
    }

    public void AttackEnd()
    {
        greatSword.collider.enabled = false;
    }
}