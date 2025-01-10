using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private BarHandler hpBar;
    [SerializeField] private GameObject deathPanel;

    [Header("Needed Managers")]
    [SerializeField] private PlayerManager playerManager;

    private void Awake()
    {
        deathPanel.SetActive(false);
        playerManager.OnPlayerTookDamage += OnPlayerTakeDamage;
        playerManager.OnPlayerDeath += OnPlayerDeath;
        playerManager.OnPlayerLoad += BarValuesSetUp;
    }
    public void BarValuesSetUp(BarArgs barArgs)
    {
        hpBar.SetUpSlider(barArgs);
    }

    public void OnPlayerTakeDamage(int value)
    {
        hpBar.modifyValue(-value);
    }

    public void OnPlayerHeal(int value)
    {
        hpBar.modifyValue(value);
    }
    public void OnPlayerDeath()
    {
        deathPanel.SetActive(true);
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
