using System.Collections.Generic;
using UnityEngine;

//If need to implement different variations of the npc can use dictionary with queue
[DisallowMultipleComponent]
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectReference;

    [Min(0)]
    [SerializeField] private int preSpawnedCount;

    private Queue<GameObject> _objectQueue = new Queue<GameObject>();

    private void Start()
    {
        if (preSpawnedCount == 0) return;

        for (int i = 0; i < preSpawnedCount; i++)
        {
            GameObject go = Instantiate(objectReference);
            _objectQueue.Enqueue(go);
            go.SetActive(false);
        }

        Debug.Log(_objectQueue.Count);
    }
 
    public GameObject PoolObject() 
    {

        if (_objectQueue.Count > 0)
        {
            GameObject go = _objectQueue.Dequeue();
            go.SetActive(true);
            Debug.Log(_objectQueue.Count);            
            return go;
        }
        else
        {
            GameObject go = Instantiate(objectReference);
            return go;
        }
    }

    public void ReturnObject(GameObject go)
    {
        _objectQueue.Enqueue(go);
        go.SetActive(false);
    }

}
