using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] public GameObject objectPrefab;
    public int poolSize = 10;
    private List<GameObject> objectPool = new List<GameObject>();

    void Start()
    {
        // Instantiate the enemy pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        // Find an inactive obj in the pool
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If all enemies are in use, instantiate a new one
        GameObject newObject = Instantiate(objectPrefab);
        objectPool.Add(newObject);
        return newObject;
    }

    public GameObject GetObjectFromPool(Transform _transform)
    {
        // Find an inactive obj in the pool
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = _transform.position;
                obj.SetActive(true);
                return obj;
            }
        }

        // If all enemies are in use, instantiate a new one
        GameObject newObject = Instantiate(objectPrefab, _transform);
        objectPool.Add(newObject);
        return newObject;
    }
}