
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace HW2
{
    public class PlayerMovementHandler : PlayerHandlerScript
    {
        [SerializeField] private NavMeshAgent agent;

        private Vector3 _destiantion;
        private bool _isMoving = false;

        private void Awake()
        {
            agent.speed = PlayerSettings.MovementSpeed;
            agent.acceleration = PlayerSettings.AccelerationSpeed;
        }

        private void Update()
        {
            if (!_isMoving) return;

            if (WasDestinationReached())
            {
                StopMoving();
            }
        }

        public void Move()
        {
            int groundLayerask = LayerMask.GetMask("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 80f, groundLayerask))
            {
                _destiantion = hit.point;
                Debug.DrawLine(ray.origin, _destiantion);

                agent.SetDestination(_destiantion);
                _isMoving = true;
            }
        }

        private bool WasDestinationReached()
        {
            Vector3 playerGroundPosition = new Vector3(transform.position.x, _destiantion.y, transform.position.z);
            return PlayerSettings.DestinationOffset >= (_destiantion - playerGroundPosition).sqrMagnitude;
        }

        public void StopMoving()
        {
            _isMoving = false;
            agent.ResetPath();
        }
    }
}

