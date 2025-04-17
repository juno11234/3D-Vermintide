using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWepon : MonoBehaviour
{
    [SerializeField]
    private int damage=5;

    private Collider collider;
    private GoblinBase goblin;

    private void Start()
    {
        collider = GetComponent<Collider>();
        goblin = GetComponentInParent<GoblinBase>();
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player.CurrentPlayer.MainCollider))
        {
            Debug.Log("고블린 공격!");
            CombatEvents e = new CombatEvents();
            e.Damage = damage;
            e.HitPosition = other.ClosestPoint(transform.position);
            e.Sender = goblin;
            e.Receiver = Player.CurrentPlayer;

            CombatSystem.Instance.AddInGameEvent(e);
        }
    }
}