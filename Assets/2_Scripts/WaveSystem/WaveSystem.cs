using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSystem : MonoBehaviour
{
    //웨이브 소환관리 (종류, 타이머)
    //플레이어와 spawnPoint중 가장 가까운 곳 두곳을 찾아 웨이브소환
    //웨이브 종류는 타이머웨이브, 트리거 웨이브, 엘리트 웨이브
    //오브젝트 풀에서 고블린 1과 고블린 2를 섞어서 소환 
    public enum WaveType
    {
        TimerWave,
        TriggerWave,
        ElliteWave
    }

    [System.Serializable]
    public class WaveSetting
    {
        public Transform[] spawnPoint;
        public float lastWaveTimer = 0f;
        public float waveDelay;
        public int spawnAmount;
        public float defalutWaveEliteRate;
        public int eliteSpawnAmount;
        public float eliteWaveRate;
    }

    public WaveSetting set;

    private void Awake()
    {
        set = new WaveSetting();
    }

    void Update()
    {
        set.lastWaveTimer += Time.deltaTime;
        if (set.lastWaveTimer < set.waveDelay) return;
        WaveStart();
    }

    public void WaveStart()
    {
        set.lastWaveTimer = 0f;
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        for (int i = 0; i < set.spawnAmount; i++)
        {
            DefaultWave();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DefaultWave()
    {
        IObjectPoolItem horde;

        if (Random.value <= set.defalutWaveEliteRate / 100f)
        {
            horde = ObjectPoolManager.Instance.GetObjectOrNull("Goblin_Elite");
        }
        else
        {
            string goblinType = Random.Range(0, 2) == 0 ? "Goblin_1" : "Goblin_2";
            horde = ObjectPoolManager.Instance.GetObjectOrNull(goblinType);
        }

        horde.GameObject.transform.position = transform.position;
        horde.GameObject.SetActive(true);
    }
}