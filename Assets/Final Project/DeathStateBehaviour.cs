using UnityEngine;

namespace HW3
{
    public class DeathStateBehaviour : StateMachineBehaviour
    {
        [SerializeField] private GameObject bloodParticleEffect; 
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Entered Death Animation");

            if (bloodParticleEffect != null)
            {
                Vector3 spawnPosition = animator.transform.position + Vector3.up; 
                GameObject bloodEffect = Instantiate(bloodParticleEffect, spawnPosition, Quaternion.identity);

                
                ParticleSystem ps = bloodEffect.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); 
                    ps.Play(); 
                 
                }
               

                Destroy(bloodEffect, 2f); 
            }
           
        }
    }

}
