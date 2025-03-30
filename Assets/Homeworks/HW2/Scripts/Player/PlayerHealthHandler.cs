using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HW2
{
    public class PlayerHealthHandler : PlayerHandlerScript
    {
        public event UnityAction<int> OnPlayerTookDamage;
        public event UnityAction<float> OnPlayerHealthChange;
        public event UnityAction OnPlayerDeath;

        private int _currentHP;
        private bool _isInvul = false;

        private Coroutine _invulCoroutine;

        private void Awake()
        {
            _currentHP = PlayerSettings.MaxHP;
            playerController.OnPlayerHitAction += TakeDamage;

            playerController.EffectActions[PowerUpType.Heal] += Heal;
            playerController.EffectActions[PowerUpType.Invulnerable] += ActivateInvul;
        }

        public void TakeDamage(int damage)
        {
            if (_isInvul) return;

            ActivateInvul(PlayerSettings.InvulDuration);

            _currentHP -= damage;

            if (_currentHP <= 0 )
            {
                _currentHP = 0;
                OnPlayerDeath?.Invoke(); 
            }

            OnPlayerHealthChange.Invoke((float)_currentHP/ PlayerSettings.MaxHP);
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

            OnPlayerHealthChange.Invoke((float)_currentHP / PlayerSettings.MaxHP);
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
}

