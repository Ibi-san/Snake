using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float Speed => _speed;

    [SerializeField] private Tail _tailPrefab;
    [SerializeField] private Transform _head;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<MeshRenderer> _meshes;
    private Tail _tail;
    
    public void Init(int detailCount, float hue)
    {
        SetColor(hue);
        _tail = Instantiate(_tailPrefab, transform.position, Quaternion.identity);
        _tail.SetColor(hue);
        _tail.Init(_head, _speed, detailCount);
    }

    public void SetDetailCount(int detailCount) => _tail.SetDetailCount(detailCount);

    public void SetRotation(Vector3 pointToLook) => _head.LookAt(pointToLook);

    private void SetColor(float hue)
    {
        Color.RGBToHSV(_meshes[0].material.color, out float h,out float s, out float v);
        Color newColor = Color.HSVToRGB(hue, s, v);
        foreach (var mesh in _meshes)
        {
            mesh.material.color = newColor;
        }
    }

    private void Update() => Move();

    private void Move() => transform.position += _head.forward * (Time.deltaTime * _speed);

    public void Destroy()
    {
        _tail.Destroy();
        Destroy(gameObject);
    }
}