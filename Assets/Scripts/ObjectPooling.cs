using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject bullet;

    private void Start()
    {
        CreatePool(bullet, 20);
    }
    private void CreatePool(GameObject prefab, int poolSize)
    {
        Queue<GameObject> newPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.GetComponent<PrefabIdentifier>().SetPrefab(prefab);
            obj.SetActive(false);
            newPool.Enqueue(obj);
        }

        pools[prefab] = newPool;
    }

    public void ActivateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Queue<GameObject> pool = pools[prefab];
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
    }

    public void RemoveObject(GameObject obj)
    {
        obj.SetActive(false);
        pools[obj.GetComponent<PrefabIdentifier>().prefab].Enqueue(obj);
    }
}
