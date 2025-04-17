using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MonoBehaviour
{
    //무기 데미지, 공격시 콜라이더 활성화, 맞은 대상 몬스터 확인후 데미지

    private Collider collider;
    private int damage = 10;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
        {
            Debug.Log("공격!");
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