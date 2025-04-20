using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Transform[] spawnPoint;
    public float lastWaveTimer = 0f;
    public float waveDelay = 30f;

    void Update()
    {
        lastWaveTimer += Time.deltaTime;
        if (lastWaveTimer < waveDelay) return;
        WaveStart();
    }
    
    public void WaveStart()
    {
        lastWaveTimer = 0f;
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        var closestPoints = spawnPoint;
        
        for (int i = 0; i < 30; i++)
        {
            var horde = ObjectPoolManager.Instance.GetObjectOrNull("Goblin_1");
            
            horde.GameObject.transform.position = transform.position;
            horde.GameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}