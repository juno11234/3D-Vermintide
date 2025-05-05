using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveSystem : MonoBehaviour
{
    //웨이브 소환관리 (종류, 타이머)
    //플레이어와 spawnPoint중 가장 가까운 곳 두곳을 찾아 웨이브소환
    //웨이브 종류는 타이머웨이브, 트리거 웨이브, 엘리트 웨이브
    //오브젝트 풀에서 고블린 1과 고블린 2를 섞어서 소환 

    [System.Serializable]
    public class WaveSetting
    {
        public Transform[] spawnPoint;
        public float lastWaveTimer = 0f;
        public float waveDelay;
        public int timerWaveAmount;
        public float defalutWaveEliteRate;

        public float eliteWaveRate = 10;
        public int eliteSpawnAmount = 10;
    }

    public static WaveSystem Instance;
    public WaveSetting set;
    private Transform[] nearPoint;
    public int waveCounter = 0;

    [SerializeField]
    private SFXData waveSFX;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CombatSystem.Instance.Events.OnEnemyDieEvents += WaveCount;
    }

    void Update()
    {
        set.lastWaveTimer += Time.deltaTime;
        if (set.lastWaveTimer < set.waveDelay) return;
        WaveStart(set.timerWaveAmount);
    }

    public void WaveStart(int amount)
    {
        set.lastWaveTimer = 0f;
        nearPoint = SpawnPointSet();
        SFXManager.Instance.Play(waveSFX);
        if (Boss.CurrentBoss.isDead == false)
        {
            BGMManager.Instance.ChangeBGM(GameState.Wave);
        }

        if (waveCounter <= 0) waveCounter = 0;

        StartCoroutine(SpawnDelay(amount));
    }

    IEnumerator SpawnDelay(int amount)
    {
        if (Random.value <= set.eliteWaveRate / 100f)
        {
            waveCounter += set.eliteSpawnAmount;
            for (int i = 0; i < set.eliteSpawnAmount; i++)
            {
                ElliteWave();
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            waveCounter += amount * 2;
            for (int i = 0; i < amount; i++)
            {
                DefaultWave(nearPoint[0].position);
                DefaultWave(nearPoint[1].position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    public void WaveCount(EnemyDieEvents enemyDieEvents)
    {
        waveCounter--;
        if (waveCounter <= 1)
        {
            BGMManager.Instance.ChangeBGM(GameState.Dungeon);
        }
    }

    private void DefaultWave(Vector3 spawnPoint)
    {
        IObjectPoolItem horde;

        if (Random.value <= set.defalutWaveEliteRate / 100f)
        {
            string eliteType = Random.Range(0, 2) == 0 ? "Goblin_Elite" : "Goblin_Shaman";
            horde = ObjectPoolManager.Instance.GetObjectOrNull(eliteType);
        }
        else
        {
            string goblinType = Random.Range(0, 2) == 0 ? "Goblin_1" : "Goblin_2";
            horde = ObjectPoolManager.Instance.GetObjectOrNull(goblinType);
        }

        horde.GameObject.transform.position = spawnPoint;
        horde.GameObject.SetActive(true);
    }

    private void ElliteWave()
    {
        IObjectPoolItem horde;

        string eliteType = Random.Range(0, 2) == 0 ? "Goblin_Elite" : "Goblin_Shaman";

        horde = ObjectPoolManager.Instance.GetObjectOrNull(eliteType);
        horde.GameObject.transform.position = nearPoint[0].position;
        horde.GameObject.SetActive(true);
    }

    private Transform[] SpawnPointSet()
    {
        //스폰포인트 수만큼 배열로 만든다
        //순환하면서 플레이어와 거리를 비교한다
        //
        float[] distance = new float[set.spawnPoint.Length];
        for (int i = 0; i < set.spawnPoint.Length; i++)
        {
            distance[i] = Vector3.Distance(Player.CurrentPlayer.transform.position, set.spawnPoint[i].position);
        }

        int first = -1;
        int second = -1;
        float min = float.MaxValue;
        float min2 = float.MaxValue;

        for (int i = 0; i < set.spawnPoint.Length; i++)
        {
            if (distance[i] < min) //min보다 작다면
            {
                min2 = min; //처음것을 두번째min에 덮어씌움
                second = first;

                min = distance[i]; //작은 것을 첫번째min에
                first = i;
            }
            else if (distance[i] < min2)
            {
                min2 = distance[i];
                second = i;
            }
        }

        return new Transform[] { set.spawnPoint[first], set.spawnPoint[second] };
    }
}