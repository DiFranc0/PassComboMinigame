using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPoolManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(transform); // Set the parent to the ObjectPoolManager
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = null;
        
        Queue<GameObject> objectPool = poolDictionary[tag];
        if (objectPool.Count == 0)
        {
            Debug.LogWarning("No objects available in pool with tag " + tag);
            GameObject prefab = null;
            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    prefab = pool.prefab;
                    break;
                }
            }

            if(prefab != null)
            {
                objectToSpawn = Instantiate(prefab);
                objectToSpawn.SetActive(false);
                objectToSpawn.transform.SetParent(transform);
            }
            else
            {
                Debug.LogError("Couldn't find prefab for pool tag: " + tag);
                return null;
            }
        }
        else
        {
            // Get object from pool
            objectToSpawn = objectPool.Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // If the object has a projectile component, reset it
        IPooledProjectile pooledObj = objectToSpawn.GetComponent<IPooledProjectile>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;


    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}
