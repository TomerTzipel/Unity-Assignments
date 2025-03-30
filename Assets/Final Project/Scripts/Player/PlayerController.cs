using HW2;
using System.Collections;
using Unity.VisualScripting;
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
    private InputSystem_Actions _inputActions;
    private bool _canRecieveInput = true;
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
        _inputActions = new InputSystem_Actions();
        GameManager.Instance.OnGameTimerTick += HandleTimerTick;
        GameManager.Instance.OnGameResume += HandleResumeGame;
        GameManager.Instance.OnSaveGame += HandleGameSave;
        GameManager.Instance.OnLoadGame += HandleGameLoad;
        OnPlayerDeath += PlayerDeath;
        OnPlayerDeath += playerMovementHandler.StopMoving;
        OnPlayerFlash += playerMovementHandler.StopMoving;
        OnPlayerPowerUp += HandleSlowTime;
        _score = 0;
    }
    private void Start()
    {
        //Should be in awake, no time to deal with competitions on awake
        GameManager.Instance.StartGame();
    }

    private void OnEnable()
    {
        _inputActions.Player.MouseMove.Enable();
        _inputActions.Player.Flash.Enable();
        _inputActions.Player.Pause.Enable();
        _inputActions.Player.Pause.performed += OnPauseCommand;
        _inputActions.Player.MouseMove.performed += OnMoveCommand;
        _inputActions.Player.Flash.performed += OnFlashCommand;
    }
    private void OnDisable()
    {
        _inputActions.Player.MouseMove.performed -= OnMoveCommand;
        _inputActions.Player.Flash.performed -= OnFlashCommand;
        _inputActions.Player.MouseMove.Disable();
        _inputActions.Player.Flash.Disable();
        _inputActions.Player.Pause.Disable();
    }

    //User Input Handling

    public void OnPauseCommand(InputAction.CallbackContext context)
    {
        if (!_canRecieveInput) return;

        _canRecieveInput = false;
        GameManager.Instance.PauseGame();
    }

    public void OnMoveCommand(InputAction.CallbackContext context)
    {
        if (!_canRecieveInput) return;
        playerMovementHandler.Move();
    }

    public void OnFlashCommand(InputAction.CallbackContext context)
    {
        if (!_canRecieveInput) return;
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

        switch (effect.type)
        {
            case PowerUpType.Heal:
                AudioManager.Instance.PlaySfx(SFX.Heal);
                break;
            case PowerUpType.SlowTime:
                AudioManager.Instance.PlaySfx(SFX.SlowTime);
                break;
            case PowerUpType.Invulnerable:
                AudioManager.Instance.PlaySfx(SFX.Invul);
                break;
        }
        OnPlayerPowerUp.Invoke(effect);
    }
    private void HandleResumeGame()
    {
        _canRecieveInput = true;
    }
    private void HandleTimerTick(int secodns)
    {
        if(secodns%60 == 0) GainScore(PlayerSettings.TimerScore);
    }

    private void GainScore(int score)
    {
        _score += score;
        OnPlayerScoreGain.Invoke(_score);
    }

    private void PlayerDeath()
    {
        playerCollidor.enabled = false;
        _canRecieveInput = false;
        GameManager.Instance.StopGameTimer();
        GameManager.Instance.GameEndScore = _score;

        //Forcing the save to be unavailable, if there was one
        PlayerPrefs.SetInt("DoesSaveExist", 0);

        StartCoroutine(MoveToGameOver());
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

    private IEnumerator MoveToGameOver()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.ChangeScene(2);
    }

    private void HandleGameSave()
    {
        PlayerSaveData data = new PlayerSaveData
        {
            playerHP = playerHealthHandler.CurrentHP,
            playerScore = _score,
            playerX = transform.position.x,
            playerZ = transform.position.z
        };

        GameManager.Instance.GameSaveData.playerSaveData = data;
    }

    private void HandleGameLoad(SaveData data)
    {
        _score = data.playerSaveData.playerScore;
        OnPlayerScoreGain.Invoke(_score);

        NavMeshAgent.Warp(new Vector3(data.playerSaveData.playerX, transform.position.y, data.playerSaveData.playerZ));
    }
}
