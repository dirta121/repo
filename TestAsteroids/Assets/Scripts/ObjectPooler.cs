using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict;
    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var obj=Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag,Vector3 position,Quaternion rotation)
    {
        Queue<GameObject> queue;
        if (poolDict.TryGetValue(tag.ToLower(), out queue))
        {
            GameObject objectToSpawn = poolDict[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            poolDict[tag].Enqueue(objectToSpawn);

            IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObject!=null)
                pooledObject.OnObjectSpawn();
            return objectToSpawn;
        }
        return null;
    }

}

public interface IPooledObject
{
    void OnObjectSpawn();
}
