using UnityEngine;
using UnityEngine.InputSystem;


namespace HW1
{
    public class PlayerMovement : AgentMovement
    {

        [SerializeField] private HW2.PlayerSettings playerSettings;

        private Vector3 _destiantion;
        private bool _isMoving = false;

        private void Awake()
        {
            agent.SetAreaCost(3, 2);
            agent.SetAreaCost(10, 2);
        }

        private void Update()
        {
            if (!_isMoving) return;

            if (WasDestinationReached())
            {
                _isMoving = false;
                agent.ResetPath();
            }
        }

        public void OnMoveCommand(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            int groundLayerask = LayerMask.GetMask("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 80f, groundLayerask))
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
            return Mathf.Pow(playerSettings.DestinationOffset,2) >= (_destiantion - playerGroundPosition).sqrMagnitude;
        }
    }



}
