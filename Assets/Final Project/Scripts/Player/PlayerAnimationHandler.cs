using UnityEngine;

public class PlayerAnimationHandler : PlayerHandlerScript
{
    //Hashes:
    private static readonly int MovementSpeedAnimatorHash = Animator.StringToHash("MovementSpeedPrecentage");
    private static readonly int HurtTriggerAnimatorHash = Animator.StringToHash("HurtTrigger");
    private static readonly int DeathBoolAnimatorHash = Animator.StringToHash("IsDead");


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
        Debug.Log("Activating Death Animation!");
        animator.SetBool(DeathBoolAnimatorHash, true);
    }
}
