using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Tail _tailPrefab;
    [SerializeField] private Transform _head;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotateSpped = 90f;
    private Vector3 _targetDirection = Vector3.zero;
    private Tail _tail;
    
    public void Init(int detailCount)
    {
        _tail = Instantiate(_tailPrefab, transform.position, Quaternion.identity);
        _tail.Init(_head, _speed, detailCount);
    }

    public void LookAt(Vector3 cursorPosition) => _targetDirection = cursorPosition - _head.position;

    public void GetMoveInfo(out Vector3 position) => position = transform.position;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
        _head.rotation = Quaternion.RotateTowards(_head.rotation, targetRotation, Time.deltaTime * _rotateSpped);
    }

    private void Move() => transform.position += _head.forward * (Time.deltaTime * _speed);


    public void Destroy()
    {
        _tail.Destroy();
        Destroy(gameObject);
    }
}