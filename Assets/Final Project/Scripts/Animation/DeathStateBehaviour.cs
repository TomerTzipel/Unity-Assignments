using UnityEngine;

public class DeathStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private ParticleSystem bloodParticleEffect;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (bloodParticleEffect != null)
        {
            Vector3 spawnPosition = animator.transform.position + Vector3.up;
            ParticleSystem bloodEffect = Instantiate(bloodParticleEffect, spawnPosition, Quaternion.identity);

            if (bloodEffect != null)
            {
                bloodEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                bloodEffect.Play();

            }

            Destroy(bloodEffect.gameObject, 2f);
        }

    }
}
