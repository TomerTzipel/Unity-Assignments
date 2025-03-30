using HW2;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public struct HealthChangeArgs
{
    public int currentHealth;
    public int maxHealth;
    public bool wasHealthLost;
}

public class PlayerHealthHandler : PlayerHandlerScript
{
    //Fields:
    private int _currentHP;
    private int _maxHP;
    private bool _isInvul = false;
    private Coroutine _invulCoroutine;


    //Events:
    public event UnityAction<HealthChangeArgs> OnPlayerHealthChange;
    public event UnityAction OnPlayerDeath;


    void Awake()
    {
        _maxHP = PlayerSettings.MaxHP;
        _currentHP = _maxHP;

        playerController.OnPlayerHit += TakeDamage;
        playerController.OnPlayerPowerUp += HandlePowerUp;
    }


    private void TakeDamage(int damage)
    {
        if (_isInvul) return;

        ActivateInvul(PlayerSettings.InvulDuration);

        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            _currentHP = 0;
            OnPlayerDeath.Invoke();
        }

        OnPlayerHealthChange.Invoke(new HealthChangeArgs
        {
            currentHealth = _currentHP,
            maxHealth = _maxHP,
            wasHealthLost = true
        });
    }


    private void HandlePowerUp(Effect effect)
    {
        switch (effect.type)
        {
            case PowerUpType.Heal:
                Heal((int)effect.value);
                break;
            case PowerUpType.Invulnerable:
                ActivateInvul(effect.value);
                break;
        }
    }


    private void Heal(int value)
    {
        _currentHP += value;

        if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }

        OnPlayerHealthChange.Invoke(new HealthChangeArgs
        {
            currentHealth = _currentHP,
            maxHealth = _maxHP,
            wasHealthLost = false
        });
    }


    private void ActivateInvul(float duration)
    {
        if (_invulCoroutine != null) StopCoroutine(_invulCoroutine);
        _invulCoroutine = StartCoroutine(InvulDuration(duration));
    }


    private IEnumerator InvulDuration(float duration)
    {
        _isInvul = true;

        yield return new WaitForSeconds(duration);

        _isInvul = false;
    }
}
