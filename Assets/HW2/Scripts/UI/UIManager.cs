using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private BarHandler hpBar;
    [SerializeField] private GameObject deathPanel;

    [SerializeField] private TMP_Text scoreText;

    [Header("Needed Managers")]
    [SerializeField] private PlayerController playerController;

    private const string hitCounterText = "Score:";
    private int _score;

    private void Awake()
    {
        deathPanel.SetActive(false);
        //Subscribe to on player heal
        //Subscribe to on player shield
        //Subscribe to on player lose shield
        playerController.OnPlayerTookDamage += OnPlayerTakeDamage;
        playerController.OnPlayerDeath += OnPlayerDeath;
        playerController.OnPlayerLoad += BarValuesSetUp;

        _score = 0;
        UpdateScore(_score);
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int value)
    {
        _score += value;
        scoreText.text = hitCounterText + _score;
    }
    private void BarValuesSetUp(BarArgs barArgs)
    {
        hpBar.SetUpSlider(barArgs);
    }

    private void OnPlayerTakeDamage(int value)
    {
        hpBar.modifyValue(-value);
    }

    private void OnPlayerHeal(int value)
    {
        hpBar.modifyValue(value);
    }
    private void OnPlayerDeath()
    {
        deathPanel.SetActive(true);
    }
   
}
