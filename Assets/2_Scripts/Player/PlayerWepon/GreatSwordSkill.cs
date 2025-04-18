using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordSkill : MonoBehaviour
{
    int damage = 100;

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