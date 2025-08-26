using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GreatSwordSkill : MonoBehaviour
{

    [SerializeField] private int damage = 100;

    //사운드
    [SerializeField] private List<SFXData> hit;

    private HashSet<IFighter> damaged = new HashSet<IFighter>();
    private void OnEnable()
    {
        damaged.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Enemy") == other.gameObject.layer)
        {

            //Debug.Log("공격!");
            int index = Random.Range(0, hit.Count);

            SFXManager.Instance.Play(hit[index]);

            var monster = CombatSystem.Instance.GetMonsterOrNull(other);
            if (monster != null && damaged.Contains(monster) == false)
            {
                CombatEvents combatEvents = new CombatEvents
                {
                    Sender = Player.CurrentPlayer,
                    Receiver = monster,
                    Damage = damage,
                    HitPosition = other.ClosestPoint(transform.position),
                    Collider = other
                };

                CombatSystem.Instance.AddInGameEvent(combatEvents);
                damaged.Add(monster);
            }
        }
    }
}