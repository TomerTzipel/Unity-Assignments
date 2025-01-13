using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthHandler : PlayerHandlerScript
{
    [SerializeField] private MeshRenderer meshRenderer;
   

    public event UnityAction<int> OnPlayerTookDamage;
    public event UnityAction OnPlayerDeath;

    private int _currentHP;
    private bool _isInvul = false;

    private void Awake()
    {
        _currentHP = PlayerSettings.MaxHP;
        playerController.OnPlayerHitAction += TakeDamage;

        playerController.EffectActions[EffectType.Heal] += Heal;
        playerController.EffectActions[EffectType.Invul] += ActivateInvul;
    }

    public void TakeDamage(int damage)
    {
        if (_isInvul) return;

        ActivateInvul(PlayerSettings.InvulDuration);

        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            OnPlayerDeath.Invoke();
        }

        OnPlayerTookDamage.Invoke(damage);
    }

    private void Heal(float value)
    {
        //must recieve float becuase of the event
        _currentHP += (int)value;

        if (_currentHP > PlayerSettings.MaxHP)
        {
            _currentHP = PlayerSettings.MaxHP;
        }
    }

    private void ActivateInvul(float duration)
    {
        StartCoroutine(InvulDuration(duration));
    }

    private IEnumerator InvulDuration(float duration)
    {
        _isInvul = true;
        meshRenderer.material = PlayerSettings.HurtMaterial;

        yield return new WaitForSeconds(duration);

        _isInvul = false;
        meshRenderer.material = PlayerSettings.PlayerMaterial;
    }
}
