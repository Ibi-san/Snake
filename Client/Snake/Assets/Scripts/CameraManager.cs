using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform _camera;
    private void Start()
    {
        _camera = Camera.main.transform;
        _camera.parent = transform;
        _camera.localPosition = Vector3.zero;
    }

    private void OnDestroy()
    {
        if (Camera.main == null) return; 
        _camera.parent = null;
    }
}
