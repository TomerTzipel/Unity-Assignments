using UnityEngine;
namespace HW1
{
    public class AiMovement : AgentMovement
    {
        [SerializeField] GameObject target;

        private void Awake()
        {
            agent.SetDestination(target.transform.position);
        }

    }
}

