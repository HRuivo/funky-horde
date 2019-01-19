using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public int size = 20;
    public GameObject instancePrefab;

    private List<Transform> _pool;
    private List<int> _freeindices;


    void Awake()
    {
        Init();
    }

    void Init()
    {
        _freeindices = new List<int>(size);
        for (int i = 0; i < _freeindices.Capacity; i++)
        {
            _freeindices.Add(i);
        }

        _pool = new List<Transform>(size);

        for (int i = 0; i < size; i++)
        {
            GameObject newInstance = Instantiate<GameObject>(instancePrefab);
            newInstance.transform.parent = transform;
            newInstance.transform.position = Vector3.zero;
            newInstance.GetComponent<IPoolItem>().ID = i;
            newInstance.SetActive(false);

            _pool.Add(newInstance.transform);
        }
    }

    public Transform Spawn(Vector3 location)
    {
        Transform result = Spawn();
        if (result)
        {
            result.position = location;
            result.GetComponent<Enemy>().Reset();
        }

        return result;
    }

    public Transform Spawn()
    {
        if (_freeindices.Count > 0)
        {
            int freeIndex = _freeindices[0];
            _freeindices.RemoveAt(0);

            Transform instance = _pool[freeIndex];
            instance.GetComponent<IPoolItem>().ID = freeIndex;
            instance.parent = null;
            instance.gameObject.SetActive(true);

            return instance;
        }
        else
        {
            return null;
        }
    }

    public void Despawn(Transform instance)
    {
        instance.gameObject.SetActive(false);
        instance.parent = transform;
        _freeindices.Add(instance.GetComponent<IPoolItem>().ID);
    }
}
