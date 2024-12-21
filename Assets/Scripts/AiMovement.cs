using UnityEngine;

public class AiMovement : AgentMovement
{
    [SerializeField] GameObject target;

    private void Awake()
    {
        agent.SetDestination(target.transform.position);
    }

}
