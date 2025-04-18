using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolItem
{
    string Key { get; set; }
    GameObject GameObject { get; }

    void ReturnToPool();
}

public class ObjectPool
{
    private Queue<IObjectPoolItem> Pool { get; set; }
    private IObjectPoolItem Item { get; set; }
    private Transform Parent { get; set; }
    private byte ExpandSize { get; set; }

    public void Initialize(IObjectPoolItem item, Transform parent, byte expandSize, string key)
    {
        Pool = new Queue<IObjectPoolItem>();
        Item = item;
        Item.GameObject.SetActive(false);
        Parent = parent;
        ExpandSize = expandSize;

        Item.Key = key;

        Expand();
    }

    public IObjectPoolItem GetItem()
    {
        if (Pool.Count == 0)
        {
            Expand();
        }

        return Pool.Dequeue();
    }

    private void Expand()
    {
        for (int i = 0; i < ExpandSize; i++)
        {
            var instance = GameObject.Instantiate(Item.GameObject, Parent).GetComponent<IObjectPoolItem>();
            instance.Key = Item.Key;
            Return(instance);
        }
    }

    public void Return(IObjectPoolItem item)
    {
        item.GameObject.SetActive(false);
        Pool.Enqueue(item);
    }
}