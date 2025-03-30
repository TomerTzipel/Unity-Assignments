using System.Collections;
using UnityEngine;

public enum Axis { x, y, z }
public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    [SerializeField] private Axis _axis;
    [SerializeField] private float _startDirection;

    private Vector3 _direction;


    void Awake()
    {
        if (_axis == Axis.x)
        {
            _direction = new Vector3(_startDirection, 0f, 0f);
        }
        if (_axis == Axis.z)
        {
            _direction = new Vector3(0f, 0f, _startDirection);
        }
        StartCoroutine(ChangeDirectionTimer());
    }

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _direction);
    }

    private IEnumerator ChangeDirectionTimer()
    {
        yield return new WaitForSeconds(_duration);
        _direction *= -1f;
        StartCoroutine(ChangeDirectionTimer());
    }
}
