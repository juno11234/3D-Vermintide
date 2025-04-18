using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    //오브젝트 풀에서 고블린 1과 고블린 2를 섞어서 소환 
    public Transform spawnPoint;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator SpawnDelay()
    {
        for (int i = 0; i < 30; i++)
        {
            var horde = ObjectPoolManager.Instance.GetObjectOrNull("Goblin_1");
            horde.GameObject.transform.position = spawnPoint.position;
            horde.GameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}