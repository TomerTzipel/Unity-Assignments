using HW1;
using HW2;
using UnityEngine;
using UnityEngine.AI;

namespace HW3
{


    public class PlayerAnimationHandler : PlayerHandlerScript
    {
        private static readonly int MovementSpeedAnimatorHash = Animator.StringToHash("MovementSpeedPrecentage");
        private static readonly int PowerUpTypeAnimatorHash = Animator.StringToHash("PowerUpType");
        private static readonly int PowerUpTriggerAnimatorHash = Animator.StringToHash("PowerUpTrigger");
        private static readonly int HurtTriggerAnimatorHash = Animator.StringToHash("HurtTrigger");
        private static readonly int DeathTriggerAnimatorHash = Animator.StringToHash("DeathTrigger");


        //The more damage the player takes the higher the layer weight of the hurt animation, this is parameter identifies the max damage value for a weight of 1
        [SerializeField] private float maxDamageThreshold;

        [Header("Required Components")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            playerController.OnPlayerPowerUp += ActivatePowerUpAnimation;
            playerController.OnPlayerTookDamage += ActivateHurtAnimation;
            playerController.OnPlayerDeath += ActivateDeathAnimation;
        }

       
        private void Update()
        {
            if (animator)
            {
                animator.SetFloat(MovementSpeedAnimatorHash, agent.velocity.magnitude / agent.speed);
            }
        }

        private void ActivatePowerUpAnimation(PowerUpType type)
        {
            animator.SetLayerWeight(1, 1);
            animator.SetInteger(PowerUpTypeAnimatorHash, (int)type);
            animator.SetTrigger(PowerUpTriggerAnimatorHash);
        }

        private void ActivateHurtAnimation(int damage)
        {
            float weight = Mathf.Clamp(damage / maxDamageThreshold, 0, 1);
            Debug.Log(weight);
            animator.SetLayerWeight(1, weight);
            animator.SetTrigger(HurtTriggerAnimatorHash);
        }

        public void ActivateDeathAnimation()
        {
            Debug.Log("Activating Death Animation!");
            animator.SetBool("IsDead", true);
            agent.isStopped = true;


        }
    }
}

