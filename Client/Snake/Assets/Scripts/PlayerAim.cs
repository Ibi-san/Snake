using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 90f;
    private Vector3 _targetDirection = Vector3.zero;
    private float _speed;

    public void Init(float speed) => _speed = speed;

    public void GetMoveInfo(out Vector3 position) => position = transform.position;

    public void SetTargetDirection(Vector3 pointToLook) => _targetDirection = pointToLook - transform.position;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);
    }

    private void Move() => transform.position += transform.forward * (_speed * Time.deltaTime);
}
