using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] private PlayerController playerController;

    [Header("UI Elements")]
    [SerializeField] private BarHandler hpBar;
    [SerializeField] private BarHandler flashCooldownBar;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    //Fields:
    private const string ScoreText = "Score:";
    
    private void Awake()
    {
        hpBar.UpdateSlider(1f, playerController.PlayerSettings.MaxHP, playerController.PlayerSettings.MaxHP);
        playerController.OnPlayerHealthChange += OnPlayerHealthChange;
        playerController.OnPlayerFlash += HandlePlayerFlash;
        playerController.OnPlayerScoreGain += HandleScoreUpdate;
        HandleScoreUpdate(0);
    }
    private void Start()
    {
        GameManager.Instance.OnGameTimerTick += UpdateTimer;
    }
    private void HandlePlayerFlash()
    {
        flashCooldownBar.FillInDuration(playerController.PlayerSettings.FlashCD);
    }

    private void HandleScoreUpdate(int score)
    {
        scoreText.text = ScoreText + score;
    }

    private void OnPlayerHealthChange(HealthChangeArgs args)
    {
        float hpPercentage = (((float)args.currentHealth) / args.maxHealth);
        hpBar.UpdateSlider(hpPercentage, args.currentHealth, args.maxHealth);
    }

    private void UpdateTimer(int totalSeconds)
    {
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        timerText.text = $"{minutes}:{seconds}";
    }
}
