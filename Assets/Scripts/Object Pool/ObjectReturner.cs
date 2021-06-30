using UnityEngine;

public class ObjectReturner : MonoBehaviour
{
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    public void ReturnObject()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(gameObject);
        }
    }
}
