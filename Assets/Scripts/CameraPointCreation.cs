using System;
using UnityEngine;

public class CameraPointCreation : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private ObjectPool objectPool;

    private void Awake()
    {
        try
        {
            objectPool = FindObjectOfType<ObjectPool>();
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("Add ObjectPool class to the scene");
        }
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateCameraPoint();
        }
    }

    private void CreateCameraPoint()
    {
        Vector3 mousePos = new Vector3()
        {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = _camera.nearClipPlane
        };

        Ray worldRay = _camera.ScreenPointToRay(mousePos);
        
        if (Physics.Raycast(worldRay, out var hit))
        {
            GameObject go = objectPool.PoolObject();
            go.transform.position = hit.point;
        }

    }
}
