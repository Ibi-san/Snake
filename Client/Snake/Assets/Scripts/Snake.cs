using UnityEngine;

public class Snake : MonoBehaviour
{
    public float Speed => _speed;

    [SerializeField] private Tail _tailPrefab;
    [SerializeField] private Transform _head;
    [SerializeField] private float _speed = 2f;
    private Tail _tail;
    
    public void Init(int detailCount)
    {
        _tail = Instantiate(_tailPrefab, transform.position, Quaternion.identity);
        _tail.Init(_head, _speed, detailCount);
    }

    public void SetDetailCount(int detailCount) => _tail.SetDetailCount(detailCount);

    public void SetRotation(Vector3 pointToLook) => _head.LookAt(pointToLook);

    private void Update() => Move();

    private void Move() => transform.position += _head.forward * (Time.deltaTime * _speed);

    public void Destroy()
    {
        _tail.Destroy();
        Destroy(gameObject);
    }
}