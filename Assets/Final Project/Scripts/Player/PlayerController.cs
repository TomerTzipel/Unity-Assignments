using HW2;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Serialized Fields:
    [field: SerializeField] public PlayerSettings PlayerSettings{ get; private set;}
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [SerializeField] private Collider playerCollidor;

    [Header("Handlers")]
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    [SerializeField] private PlayerHealthHandler playerHealthHandler;
    [SerializeField] private PlayerFlashHandler playerFlashHandler;

    //Fields:
    private int _score;

    //Events:
    public event UnityAction<HealthChangeArgs> OnPlayerHealthChange { add { playerHealthHandler.OnPlayerHealthChange += value; } remove { playerHealthHandler.OnPlayerHealthChange -= value; } }
    public event UnityAction OnPlayerDeath { add { playerHealthHandler.OnPlayerDeath += value; } remove { playerHealthHandler.OnPlayerDeath -= value; } }
    public event UnityAction OnPlayerFlash { add { playerFlashHandler.OnPlayerFlash += value; } remove { playerFlashHandler.OnPlayerFlash -= value; } }

    public event UnityAction<int> OnPlayerHit;

    public event UnityAction<Effect> OnPlayerPowerUp;
    public event UnityAction<int> OnPlayerScoreGain;

    private void Awake()
    {
        OnPlayerDeath += PlayerDeath;
        OnPlayerDeath += playerMovementHandler.StopMoving;
        OnPlayerFlash += playerMovementHandler.StopMoving;
        OnPlayerPowerUp += HandleSlowTime;

        _score = 0;
    }


    //User Input Handling
    public void OnMoveCommand(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerMovementHandler.Move();
    }


    public void OnFlashCommand(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerFlashHandler.Flash();
    }

    //Event Invoking/Handling
    public void HitPlayer(int damage)
    {
        GainScore(PlayerSettings.HitScore);
        OnPlayerHit.Invoke(damage);
    }

    public void ActivateEffect(Effect effect)
    {
        GainScore(PlayerSettings.PowerUpScore);
        OnPlayerPowerUp.Invoke(effect);
    }

    private void HandleTimerTick(int secodns)
    {
        if(secodns%60 == 0) GainScore(PlayerSettings.TimerScore);
    }

    private void GainScore(int score)
    {
        _score += score;
        OnPlayerScoreGain.Invoke(score);
    }


    private void PlayerDeath()
    {
        playerCollidor.enabled = false;
        Debug.Log("PlayerDeath() triggered!");
    }

    private void HandleSlowTime(Effect effect)
    {
        if (effect.type != PowerUpType.SlowTime) return;

        StartCoroutine(SlowTimeCoroutine(effect.value,PlayerSettings.SlowTimeMultiplier));
    }

    private IEnumerator SlowTimeCoroutine(float duration,float timeScale)
    {
        GameManager.Instance.ChangeTimeScale(timeScale);
        yield return new WaitForSecondsRealtime(duration);
        GameManager.Instance.ChangeTimeScale(1f);
    }
}
