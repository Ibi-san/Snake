using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform _camera;
    public void Init(float offsetY)
    {
        _camera = Camera.main.transform;
        _camera.parent = transform;
        _camera.localPosition = Vector3.up * offsetY;
    }

    private void OnDestroy()
    {
        if (Camera.main == null) return; 
        _camera.parent = null;
    }
}
