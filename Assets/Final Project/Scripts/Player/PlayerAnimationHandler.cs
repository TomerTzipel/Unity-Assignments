using UnityEngine;

public class PlayerAnimationHandler : PlayerHandlerScript
{
    //Hashes:
    private static readonly int MovementSpeedAnimatorHash = Animator.StringToHash("MovementSpeedPercentage");
    private static readonly int HurtTriggerAnimatorHash = Animator.StringToHash("HurtTrigger");
    private static readonly int DeathTriggerAnimatorHash = Animator.StringToHash("DeadTrigger");


    //Serialized Fields:
    [SerializeField] private Animator animator; 

    private void Awake()
    {
        playerController.OnPlayerHealthChange += ActivateHurtAnimation;
        playerController.OnPlayerDeath += ActivateDeathAnimation;  
    }


    private void Update()
    {
        if (animator)
        {
            animator.SetFloat(MovementSpeedAnimatorHash, NavMeshAgent.velocity.magnitude / NavMeshAgent.speed);
        }
    }


    private void ActivateHurtAnimation(HealthChangeArgs args)
    {
        if (args.wasHealthLost)
        {
            animator.SetTrigger(HurtTriggerAnimatorHash);
        }     
    }

    private void ActivateDeathAnimation()
    {
        animator.SetTrigger(DeathTriggerAnimatorHash);
    }
}
