using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [System.Serializable]
    public class ObjectPoolData
    {
        public string Key;
        public GameObject Prefab;
        public int ExpandSize;
        public Transform Parent;
    }

    public ObjectPoolData[] ObjectPoolDatas;

    private Dictionary<string, ObjectPool> objectPoolDictionary
        = new Dictionary<string, ObjectPool>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (var data in ObjectPoolDatas)
        {
            CreateObjectPool(data);
        }
    }

    public void CreateObjectPool(ObjectPoolData data)
    {
        if (objectPoolDictionary.ContainsKey(data.Key))
        {
            Debug.LogWarning($"{data.Key}값이 이미존재!");
            return;
        }

        var pool = new ObjectPool();
        var poolItem = data.Prefab.GetComponent<IObjectPoolItem>();

        if (poolItem == null)
        {
            Debug.LogWarning($"{data.Prefab.name}에 인터페이스 상속받은 컴포넌트 없음");
            return;
        }

        Transform parent = data.Parent == null ? transform : data.Parent.transform;
        pool.Initialize(poolItem, parent, data.ExpandSize, data.Key);
        objectPoolDictionary.Add(data.Key, pool);
    }

    public IObjectPoolItem GetObjectOrNull(string key)
    {
        if (objectPoolDictionary.ContainsKey(key) == false)
        {
            Debug.LogWarning($"{key}값의 pool이 없음");
            return null;
        }

        return objectPoolDictionary[key].GetItem();
    }

    public void ReturnToPool(IObjectPoolItem item)
    {
        if (objectPoolDictionary.ContainsKey(item.Key) == false)
        {
            Debug.LogWarning($"{item.Key}값의 pool이 없음");
            return;
        }

        objectPoolDictionary[item.Key].Return(item);
    }
}