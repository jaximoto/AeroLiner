using UnityEngine;
using System.Collections.Generic;
using static SpawnArea;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pool;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach(Pool pool in pool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.prefab.name, objectPool);
        }
    }




    //spawn function. takes a spawnable type, and spawn coordinates, and activates an item from that pool in those coordinates
    public GameObject SpawnFromPool(string tag, Vector2 position, float rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log($"pool with tag {tag} doesn't exist");
            return null;
        }


        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.Euler(0, 0, rotation);

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}
