using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;

    [SerializeField] private float _speed = 40f;

    void Start()
    {
        _startPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    void FixedUpdate()
    {
        _progress = Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition.WithAxis(Axis.Z,-1);
    }

    public void SetBulletSpeed(float speed)
    {
        if (speed < 0) return;
        _speed = speed;
    }
}
