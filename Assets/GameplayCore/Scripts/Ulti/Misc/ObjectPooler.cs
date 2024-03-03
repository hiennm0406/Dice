using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public List<GameObject> objectInPool;
    public float lastUsed;
}

public class ObjectPooler : Singleton<ObjectPooler>
{
    private Dictionary<int, ObjectPoolItem> PoolStorage;
    private float time;

    private void OnEnable()
    {
        PoolStorage = new Dictionary<int, ObjectPoolItem>();
    }

    public GameObject GetPooledObject(int id, GameObject sample)
    {
        foreach (var item in PoolStorage)
        {
            if (item.Key == id)
            {
                foreach (var obj in item.Value.objectInPool)
                {
                    if (!obj.activeInHierarchy)
                    {
                        item.Value.lastUsed = Time.timeSinceLevelLoad;
                        return obj;
                    }
                }

                GameObject objAdd = Instantiate(sample);
                objAdd.SetActive(false);
                item.Value.objectInPool.Add(objAdd);
                item.Value.lastUsed = Time.timeSinceLevelLoad;
                return objAdd;
            }
        }
        ObjectPoolItem pool = new ObjectPoolItem();

        pool.objectToPool = sample;
        pool.objectInPool = new List<GameObject>();
        GameObject _obj = Instantiate(sample);
        _obj.SetActive(false);
        pool.lastUsed = Time.timeSinceLevelLoad;
        pool.objectInPool.Add(_obj);

        PoolStorage.Add(id, pool);

        return _obj;
    }

    private void Update()
    {
        //update each 60s
        if (Time.timeSinceLevelLoad - time < 60)
        {
            return;
        }

        foreach (var item in PoolStorage)
        {
            // clear half of anything dont use for await
            if (Time.timeSinceLevelLoad - item.Value.lastUsed > 180)
            {
                for (int i = item.Value.objectInPool.Count - 1; i > item.Value.objectInPool.Count / 2f; i--)
                {
                    Destroy(item.Value.objectInPool[i]);
                    item.Value.objectInPool.RemoveAt(i);
                    item.Value.lastUsed = Time.timeSinceLevelLoad;
                }
            }
        }
        time = Time.timeSinceLevelLoad;
    }
}