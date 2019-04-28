using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour {

    [SerializeField] private List<GameObject> _objectList;

    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int totalObjectInPool;

    private static Dictionary<int, ObjectPool> pools = new Dictionary<int, ObjectPool>();

    private void Init() {
        
        _objectList = new List<GameObject>(totalObjectInPool);
        
        for (int i = 0; i < totalObjectInPool; i++) {
            GameObject objectToSpawn = Instantiate(_gameObject);
            objectToSpawn.transform.parent = transform;
            objectToSpawn.SetActive(false);
            
            _objectList.Add(objectToSpawn);
        }
        
        pools.Add(_gameObject.GetInstanceID(), this);
    }

    private GameObject PoolObject(Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) {
        var objectToSpawn = (from item in _objectList
                             where item.activeSelf == false
                             select item).FirstOrDefault();

        if (objectToSpawn == null) {
            objectToSpawn = Instantiate(_gameObject, position, rotation);
            objectToSpawn.transform.parent = transform;
            _objectList.Add(objectToSpawn);
        } else {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);
        }

        return objectToSpawn;
    }

    static public bool IsPoolReady(GameObject original) { return pools.ContainsKey(original.GetInstanceID()); }

    static public void InitPool(GameObject original, int poolSize = 200) {
        if (!pools.ContainsKey(original.GetInstanceID())) {
            GameObject go = new GameObject("Object pool: " + original.name);
            ObjectPool pool = go.AddComponent<ObjectPool>();
            pool._gameObject = original;
            pool.totalObjectInPool = poolSize;
            pool.Init();

        }
    }

    public static GameObject GetInstance(int instanceID, Vector3 position = default(Vector3),
                                         Quaternion rotation = default(Quaternion), int poolsize = 200) {
        return pools[instanceID].PoolObject(position, rotation);
    }

    public static GameObject GetInstance(GameObject toPool, Vector3 position =  default(Vector3), Quaternion rotation = default(Quaternion), int poolSize = 200) {

        int id = toPool.GetInstanceID();
        InitPool(toPool, poolSize);
        return pools[id].PoolObject(position, rotation);
    }

    public static void Release(GameObject obj) {
        if (!obj.GetComponentInParent<ObjectPool>()) {
            foreach (var objectPool in pools.Values) {
                if (objectPool._objectList.Contains(obj)) {
                    obj.transform.parent = objectPool.transform;
                    break;
                }
            }
        }
        
        obj.SetActive(false);
    }

    public static void ClearPool() {
        foreach (var item in pools)
        {
            item.Value._objectList.Clear();
        }
        
        pools.Clear();
    }
}
