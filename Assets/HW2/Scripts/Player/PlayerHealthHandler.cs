using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public event UnityAction<int> OnPlayerTookDamage;
    public event UnityAction OnPlayerDeath;

    private int _currentHP;
    private bool _isInvul = false;
    public PlayerSettings PlayerSettings { get; set; }

    private void Awake()
    {
        _currentHP = PlayerSettings.MaxHP;
    }

    public void TakeDamage(int damage)
    {
        if (_isInvul) return;

        StartCoroutine(ActivateInvul(PlayerSettings.InvulDuration));

        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            OnPlayerDeath.Invoke();
        }

        OnPlayerTookDamage.Invoke(damage);
    }

    private IEnumerator ActivateInvul(float duration)
    {
        _isInvul = true;
        meshRenderer.material = PlayerSettings.HurtMaterial;

        yield return new WaitForSeconds(duration);

        _isInvul = false;
        meshRenderer.material = PlayerSettings.PlayerMaterial;
    }
}
