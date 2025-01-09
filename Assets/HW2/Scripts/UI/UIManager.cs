using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BarHandler hpBar;
    [SerializeField] private GameObject deathPanel;

    private void Awake()
    {
        deathPanel.SetActive(false);
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
        Debug.Log("In UI Death");
        deathPanel.SetActive(true);
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
