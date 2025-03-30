using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timeText;

    private void Awake()
    {
        _scoreText.text = $"Score - {GameManager.Instance.GameScore}";
        int seconds = GameManager.Instance.TimerSeconds % 60;
        int minutes = GameManager.Instance.TimerSeconds / 60;
        _timeText.text = $"TIME - {minutes}:{seconds}";
    }

    public void RestartPressed()
    {
        GameManager.Instance.ChangeScene(1);
    }

    public void MainMenuPressed()
    {
        GameManager.Instance.ChangeScene(0);
    }
}
