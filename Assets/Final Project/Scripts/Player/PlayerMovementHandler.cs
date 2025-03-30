using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : PlayerHandlerScript
{
    //Fields:
    private Vector3 _destiantion;
    private bool _isMoving = false;


    void Awake()
    {

        NavMeshAgent.speed = PlayerSettings.MovementSpeed;
        NavMeshAgent.acceleration = PlayerSettings.AccelerationSpeed;
    }


    void Update()
    {
        if (!_isMoving) return;

        //Nav mesh doesn't have a on destination reached event...
        if (WasDestinationReached())
        {
            StopMoving();
        }
    }


    public void Move()
    {
        int groundLayerMask = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 80f, groundLayerMask))
        {
            _destiantion = hit.point;
            Debug.DrawLine(ray.origin, _destiantion,Color.green,5f);

            NavMeshAgent.SetDestination(_destiantion);
            playerController.gameObject.transform.rotation = Quaternion.LookRotation(_destiantion - new Vector3(transform.position.x, 0, transform.position.z));
            _isMoving = true;
        }
    }


    public void StopMoving()
    {
        _isMoving = false;
        NavMeshAgent.ResetPath();
    }


    private bool WasDestinationReached()
    {
        Vector3 playerGroundPosition = new Vector3(transform.position.x, _destiantion.y, transform.position.z);
        return PlayerSettings.DestinationOffsetSqrd >= (_destiantion - playerGroundPosition).sqrMagnitude;
    } 
}
