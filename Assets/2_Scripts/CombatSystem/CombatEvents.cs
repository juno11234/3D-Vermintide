using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InGameEvent
{
    public enum EventType
    {
        Unknown,
        Combat,
        EnemyDie
    }

    public IFighter Sender { get; set; }
    public IFighter Receiver { get; set; }
    public abstract EventType Type { get; }
}

public class CombatEvents : InGameEvent
{
    public int Damage { get; set; }
    public Vector3 HitPosition { get; set; }
    public Collider Collider { get; set; }
    public override EventType Type => EventType.Combat;
}

public class EnemyDieEvents : InGameEvent
{
    public override EventType Type => EventType.EnemyDie;
}