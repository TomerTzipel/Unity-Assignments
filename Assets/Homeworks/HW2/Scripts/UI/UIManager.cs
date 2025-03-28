using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HW2
{
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

            playerController.OnPlayerLoad += OnPlayerLoad;
            playerController.OnPlayerTookDamage += OnPlayerTakeDamage;
            playerController.EffectActions[PowerUpType.Heal] += OnPlayerHeal;
            playerController.OnPlayerDeath += OnPlayerDeath;

            //Move to game manager
            Time.timeScale = 1f;
            playerController.EffectActions[PowerUpType.SlowTime] += OnSlowTime;

            _score = 0;
            OnUpdateScore(_score);
        }

        public void OnRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnUpdateScore(int value)
        {
            _score += value;
            scoreText.text = hitCounterText + _score;
        }

        private void OnPlayerLoad(PlayerSettings playerSettings)
        {
            BarArgs args = new BarArgs { startValue = playerSettings.MaxHP, maxValue = playerSettings.MaxHP, minValue = 0 };
            hpBar.SetUpSlider(args);
        }

        private void OnPlayerTakeDamage(int value)
        {
            hpBar.modifyValue(-value);
        }

        private void OnPlayerHeal(float value)
        {
            hpBar.modifyValue((int)value);
        }
        private void OnPlayerDeath()
        {
            deathPanel.SetActive(true);
        }

        //Move to game manager
        private void OnSlowTime(float duration)
        {
            StartCoroutine(SlowTime(duration));
        }

        private IEnumerator SlowTime(float duration)
        {
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(duration);
            Time.timeScale = 1f;
        }

    }
}

