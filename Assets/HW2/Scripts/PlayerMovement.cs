
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace HW2
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float destinationOffset;

        private Vector3 _destiantion;
        private bool _isMoving = false;

        private void Update()
        {
            if (!_isMoving) return;

            if (WasDestinationReached())
            {
                StopMoving();
                agent.ResetPath();
            }
        }

        public void OnMoveCommand(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

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
            return (destinationOffset * destinationOffset) >= (_destiantion - playerGroundPosition).sqrMagnitude;
        }

        public void StopMoving()
        {
            _isMoving = false;
        }
    }
}

