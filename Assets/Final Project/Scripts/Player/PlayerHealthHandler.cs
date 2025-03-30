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

    //Serialized Fields:

    //Because the Model has two materials I need to add the same material twice,
    //and changing the materialvalue only does it for the head and legs
    [SerializeField] private Material[] invulMaterial;
    [SerializeField] private Material[] regularMaterial;
    [SerializeField] private Material[] deadMaterial;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;


    //Fields:
    public int CurrentHP { get; private set; }

    private int _maxHP;
    private bool _isInvul = false;
    private Coroutine _invulCoroutine;


    //Events:
    public event UnityAction<HealthChangeArgs> OnPlayerHealthChange;
    public event UnityAction OnPlayerDeath;


    void Awake()
    {
        _maxHP = PlayerSettings.MaxHP;
        CurrentHP = _maxHP;

        playerController.OnPlayerHit += TakeDamage;
        playerController.OnPlayerPowerUp += HandlePowerUp;
        GameManager.Instance.OnLoadGame += HandleGameLoad;
    }


    private void TakeDamage(int damage)
    {
        if (_isInvul)
        {
            AudioManager.Instance.PlaySfx(SFX.Hit);
            return;
        }
        

        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            meshRenderer.materials = deadMaterial;
            AudioManager.Instance.PlaySfx(SFX.Death);
            OnPlayerDeath.Invoke();
        }

        if (CurrentHP != 0)
        {
            ActivateInvul(PlayerSettings.InvulDuration);
            AudioManager.Instance.PlaySfx(SFX.Damage);
        }
        

        OnPlayerHealthChange.Invoke(new HealthChangeArgs
        {
            currentHealth = CurrentHP,
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
        CurrentHP += value;

        if (CurrentHP > _maxHP)
        {
            CurrentHP = _maxHP;
        }

        OnPlayerHealthChange.Invoke(new HealthChangeArgs
        {
            currentHealth = CurrentHP,
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
        meshRenderer.materials = invulMaterial;
        yield return new WaitForSeconds(duration);
        meshRenderer.materials = regularMaterial;
        _isInvul = false;
    }

    private void HandleGameLoad(SaveData data)
    {
        CurrentHP = data.playerSaveData.playerHP;
        OnPlayerHealthChange.Invoke(new HealthChangeArgs { currentHealth = CurrentHP, maxHealth = _maxHP,wasHealthLost = false});
    }
}
