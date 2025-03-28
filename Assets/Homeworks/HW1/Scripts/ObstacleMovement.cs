using System.Collections;
using UnityEngine;

namespace HW1
{
    public enum Axis { x, y, z }
    public class ObstacleMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _duration;
        [SerializeField] private Axis _axis;
        [SerializeField] private float _startDirection;
        [SerializeField] private Rigidbody _rb;

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

        void FixedUpdate()
        {
            Vector3 move = _speed * Time.fixedDeltaTime * _direction;
            _rb.MovePosition(transform.position + move);
        }

        private IEnumerator ChangeDirectionTimer()
        {
            yield return new WaitForSeconds(_duration);
            _direction *= -1f;
            StartCoroutine(ChangeDirectionTimer());
        }
    }
}


