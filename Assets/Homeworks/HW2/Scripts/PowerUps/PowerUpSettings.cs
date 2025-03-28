using UnityEngine;

namespace HW2
{
    [CreateAssetMenu(fileName = "PowerUpSettings", menuName = "Scriptable Objects/PowerUpSettings")]
    public class PowerUpSettings : ScriptableObject
    {
        [SerializeField] private float _slowDownDuration;
        [SerializeField] private float _healValue;
        [SerializeField] private float _invincibilityDuration;

        public float GetValueByPowerUpType(PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.Heal:
                    return _healValue;
                case PowerUpType.SlowTime:
                    return _slowDownDuration;
                case PowerUpType.Invincibility:
                    return _invincibilityDuration;

            }

            throw new System.Exception("No matching type");
        }
    }
}

