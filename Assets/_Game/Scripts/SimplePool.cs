using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool 
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY !");
        }

        if (!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.Preload(prefab, amount, parent);
            poolInstance[prefab.PoolType] = p;
        }
    }
    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOADED !");
            return null;
        }
        
        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType + "IS NOT PRELOADED !");
            return;
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOADED !");
            return;
        }
        poolInstance[poolType].Collect();
    }
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOADED !");
            return;
        }
        poolInstance[poolType].Release();
    }
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}
public class Pool
{
    private Transform parent;
    private GameUnit prefab;
    private Queue<GameUnit> inactives = new Queue<GameUnit>();
    private List<GameUnit> actives = new List<GameUnit>();

    public void Preload(GameUnit prefab, int amount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit = null;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            while (unit == null && inactives.Count > 0)
            {
                unit = inactives.Dequeue();
            }
            if (unit == null)
            {
                unit = GameObject.Instantiate(prefab, parent);
            }
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        unit.gameObject.SetActive(true);
        actives.Add(unit);

        return unit;
    }
    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}