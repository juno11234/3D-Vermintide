using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : WeaponBase
{
    private Animator animator; 
    
    private void Start()
    {
        Player.CurrentPlayer.hasPotion = false;
        gameObject.SetActive(false);
        animator = GetComponentInParent<Animator>();
    }

    public override void RMBClick()
    {
        if (Player.CurrentPlayer.hasPotion)
        {
            Player.CurrentPlayer.Heal(50);
            Player.CurrentPlayer.hasPotion = false;
            Player.CurrentPlayer.EquipWeaponByIndex(0);
        }
    }

    public void GetPotion()
    {
        Player.CurrentPlayer.hasPotion = true;
    }
}