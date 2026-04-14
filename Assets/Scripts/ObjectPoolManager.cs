using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public enum PoolType
    {
        None, Zombies, Bullets
    }

    public static List<PoolObjectInfo> objectPools = new List<PoolObjectInfo>();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _zombiesEmpty;
    private static GameObject _bulletsEmpty;

    private void Awake()
    {
        SetupEmpties();
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PoolObjectInfo pool = objectPools.Find(o => o.lookupString == objectToSpawn.name);

        if (pool == null)
        {
            pool = new PoolObjectInfo() { lookupString = objectToSpawn.name};
            objectPools.Add(pool);
        }

        GameObject spawnableObj = null;
        spawnableObj = pool.inactiveObjects.FirstOrDefault();
        //foreach (GameObject obj in pool.inactiveObjects)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObj = obj;
        //        break;
        //    }
        //}

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.inactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string gameObjectName = obj.name.Substring(0, obj.name.Length - 7);
        PoolObjectInfo pool = objectPools.Find(o => o.lookupString == gameObjectName);

        if (pool == null)
        {
            Debug.Log("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.inactiveObjects.Add(obj);
        }
    }

    public void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _zombiesEmpty = new GameObject("Zombies");
        _zombiesEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _bulletsEmpty = new GameObject("Bullets");
        _bulletsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }
    
    private static GameObject SetParentObject(PoolType type)
    {
        switch(type)
        {
            case PoolType.Zombies:
                return _zombiesEmpty;
            case PoolType.Bullets:
                return _bulletsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}

public class PoolObjectInfo
{
    public string lookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();
}