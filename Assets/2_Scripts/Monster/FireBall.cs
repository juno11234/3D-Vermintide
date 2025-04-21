using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;
    public float duration = 3f;
    public ShamonGoblin shamonGoblin;
    public ParticleSystem explosion;

    private void OnEnable()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player.CurrentPlayer.MainCollider))
        {
            CombatEvents e = new CombatEvents();
            e.Damage = damage;
            e.HitPosition = other.ClosestPoint(transform.position);
            e.Sender = shamonGoblin;
            e.Receiver = Player.CurrentPlayer;
            explosion.Play();
            CombatSystem.Instance.AddInGameEvent(e);
        }
    }
}