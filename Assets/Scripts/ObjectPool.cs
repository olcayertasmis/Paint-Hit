using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> PooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools;

    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].PooledObjects = new Queue<GameObject>();

            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);

                pools[j].PooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= 0 && objectType < pools.Length)
        {
            if (pools[objectType].PooledObjects.Count > 0)
            {
                GameObject obj = pools[objectType].PooledObjects.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else return null;
        }
        else return null;
    }

    public void ReturnToPool(int objectType, GameObject obj)
    {
        if (objectType >= 0 && objectType < pools.Length)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;

            obj.SetActive(false);
            pools[objectType].PooledObjects.Enqueue(obj);
        }
    }
}