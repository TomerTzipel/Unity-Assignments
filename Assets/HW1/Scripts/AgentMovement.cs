using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mud"))
        {
            agent.speed *= 0.75f;
        }

        if (other.CompareTag("SpeedBoost"))
        {
            agent.speed *= 1.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mud"))
        {
            agent.speed /= 0.75f;
        }

        if (other.CompareTag("SpeedBoost"))
        {
            agent.speed /= 1.2f;
        }
    }
}
