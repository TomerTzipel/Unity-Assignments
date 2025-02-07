using UnityEngine;
using UnityEngine.Events;

namespace HW2
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private PowerUpType type;
        [SerializeField] private PowerUpSettings _powerUpSettings;

        public event UnityAction<Effect> OnPowerUpEffect;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            OnPowerUpEffect.Invoke(new Effect { type = type, value = _powerUpSettings.GetValueByPowerUpType(type) });

            Destroy(gameObject);
        }  
    }
    public struct Effect
    {
        public PowerUpType type;
        public float value;
    }
}


