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

        StartCoroutine(ActivateInvul());

        _currentHP -= damage;
        Debug.Log(_currentHP);
        Debug.Log(_currentHP <= 0);

        if (_currentHP <= 0)
        {
            Debug.Log("dead");
            OnPlayerDeath.Invoke();
        }

        OnPlayerTookDamage.Invoke(damage);
    }

    private IEnumerator ActivateInvul()
    {
        _isInvul = true;
        meshRenderer.material = PlayerSettings.HurtMaterial;

        yield return new WaitForSeconds(PlayerSettings.InvulDuration);

        _isInvul = false;
        meshRenderer.material = PlayerSettings.PlayerMaterial;
    }
}
