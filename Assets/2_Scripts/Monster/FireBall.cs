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
    public ParticleSystem fireball;
    float currentTime = 0f;

    private void OnEnable()
    {
        Vector3 target = Player.CurrentPlayer.transform.position + Vector3.up * 1.5f;
        Vector3 direction = (target - transform.position).normalized;
        transform.forward = direction;
    }

    private void Update()
    {
        transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
        currentTime += Time.deltaTime;
        if (currentTime >= duration)
        {
            Destroy(gameObject);
        }
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
            fireball.Stop();
            fireball.Clear();
            CombatSystem.Instance.AddInGameEvent(e);
        }
        else if(other.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}