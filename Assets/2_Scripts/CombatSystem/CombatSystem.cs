using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private const int MAX_EVENT_PROCESS_COUNT = 10;

    public class Callbacks
    {
        public Action<CombatEvents> OnCombatEvent;
        public Action<EnemyDieEvents> OnEnemyDieEvents;
    }

    public static CombatSystem Instance;

    private Dictionary<Collider, IFighter> monsterDictionary
        = new Dictionary<Collider, IFighter>();

    private Queue<InGameEvent> inGameEventQueue = new Queue<InGameEvent>();

    public readonly Callbacks Events = new Callbacks();

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        int processCount = 0;

        while (inGameEventQueue.Count > 0 && processCount < MAX_EVENT_PROCESS_COUNT)
        {
            var inGameEvent = inGameEventQueue.Dequeue();

            switch (inGameEvent.Type)
            {
                case InGameEvent.EventType.Combat:
                    var combatEvent = inGameEvent as CombatEvents;
                    inGameEvent.Receiver.TakeDamage(combatEvent);
                    Events.OnCombatEvent?.Invoke(combatEvent);
                    break;
                case InGameEvent.EventType.EnemyDie:
                    var enemyDieEvents = inGameEvent as EnemyDieEvents;
                    Events.OnEnemyDieEvents?.Invoke(enemyDieEvents);
                    break;
            }

            processCount++;
        }
    }

    public void RegisterMonster(IFighter monster)
    {
        if (monsterDictionary.TryAdd(monster.MainCollider, monster) == false)
        {
            Debug.LogWarning($"{monster.GameObject.name}가 등록 " +
                             $"{monsterDictionary[monster.MainCollider]}를 대체");
        }
    }

    public void RegisterBossMonster(Collider collider, IFighter monster)
    {
        if (monsterDictionary.TryAdd(collider, monster) == false)
        {
            Debug.LogWarning($"{monster.GameObject.name}가 등록 " +
                             $"{monsterDictionary[collider]}를 대체");
        }
    }

    public IFighter GetMonsterOrNull(Collider collider)
    {
        if (monsterDictionary.ContainsKey(collider))
        {
            return monsterDictionary[collider];
        }

        return null;
    }

    public void AddInGameEvent(InGameEvent inGameEvent)
    {
        inGameEventQueue.Enqueue(inGameEvent);
    }
}