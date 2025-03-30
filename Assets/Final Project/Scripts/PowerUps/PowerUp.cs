
using UnityEngine;
using UnityEngine.Events;

public struct Effect
{
    public PowerUpType type;
    public float value;
}

public class PowerUp : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] private PowerUpType type;
    [SerializeField] private PowerUpSettings _powerUpSettings;

    //Events
    public event UnityAction<Effect> OnPowerUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OnPowerUpEffect.Invoke(new Effect { type = type, value = _powerUpSettings.GetValueByPowerUpType(type) });

        Destroy(gameObject);
    }
}
