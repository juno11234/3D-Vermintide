using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodControll : MonoBehaviour
{
    public enum BloodType
    {
        Player,
        Monster,
        Boss
    }

    void Start()
    {
        CombatSystem.Instance.Events.OnCombatEvent += PlayBlood;
    }

    private void OnDestroy()
    {
        CombatSystem.Instance.Events.OnCombatEvent -= PlayBlood;
    }

    private void PlayBlood(CombatEvents combatEvent)
    {
        switch (combatEvent.Receiver.bloodType)
        {
            case BloodControll.BloodType.Player:
                return;
            case BloodControll.BloodType.Monster:
                var blood = ObjectPoolManager.Instance.GetObjectOrNull("Blood");
                blood.GameObject.transform.position = combatEvent.HitPosition;
                blood.GameObject.SetActive(true);
                break;
            case BloodControll.BloodType.Boss:
                var Bossblood = ObjectPoolManager.Instance.GetObjectOrNull("BossBlood");
                Bossblood.GameObject.transform.position = combatEvent.HitPosition;
                Bossblood.GameObject.SetActive(true);
                break;
        }
    }
}