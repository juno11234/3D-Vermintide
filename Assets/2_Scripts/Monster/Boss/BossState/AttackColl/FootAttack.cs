using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAttack : MonoBehaviour
{
    public Collider collider;

    [SerializeField]
    int damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player.CurrentPlayer.MainCollider) && Boss.CurrentBoss.isDead == false)
        {
            CombatEvents e = new CombatEvents();
            e.Sender = Boss.CurrentBoss;
            e.Receiver = Player.CurrentPlayer;
            e.Damage = damage;
            e.HitPosition = other.ClosestPoint(transform.position);
            e.Collider = other;

            CombatSystem.Instance.AddInGameEvent(e);
            Player.CurrentPlayer.KnockBack(transform.position);
        }
    }
}