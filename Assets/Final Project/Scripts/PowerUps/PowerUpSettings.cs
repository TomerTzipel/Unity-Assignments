using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSettings", menuName = "PowerUp/PowerUpSettings")]
public class PowerUpSettings : ScriptableObject
{
    [SerializeField] private float _slowDownDuration;
    [SerializeField] private int _healValue;
    [SerializeField] private float _invincibilityDuration;
    public float GetValueByPowerUpType(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Heal:
                return (float)_healValue;
            case PowerUpType.SlowTime:
                return _slowDownDuration;
            case PowerUpType.Invulnerable:
                return _invincibilityDuration;

        }

        throw new System.Exception("No matching type");
    }
}
