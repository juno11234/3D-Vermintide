using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GreatSwordSkill : MonoBehaviour
{
    int damage = 100;

    [SerializeField]
    private List<SFXData> hit;

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
        {
            //Debug.Log("공격!");
            int index = Random.Range(0, hit.Count);
            SFXManager.Instance.Play(hit[index]);
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