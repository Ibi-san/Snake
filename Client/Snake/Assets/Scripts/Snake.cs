using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private float _spped = 2f;
    [SerializeField] private float _rotateSpped = 90f;
    private Vector3 _targetDirection = Vector3.zero;

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

    private void Move() => transform.position += _head.forward * (Time.deltaTime * _spped);

    public void LookAt(Vector3 cursorPosition) => _targetDirection = cursorPosition - _head.position;
}