using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Fields:
    public int GameScore { get; set; }
    public int TimerSeconds { get; private set; }
    private float _gameTimeScale;
    private Coroutine _gameTimerCoroutine;
   
    //Events:
    public event UnityAction<int> OnGameTimerTick;
    public event UnityAction OnGamePause;
    public event UnityAction OnGameResume;
    private void Awake()
    {
        if (!Instance.IsUnityNull())
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGameTimer()
    {
        TimerSeconds = 0;
        _gameTimerCoroutine = StartCoroutine(RunTimer());
    }
    public void StopGameTimer()
    {
        StopCoroutine(_gameTimerCoroutine);
        _gameTimerCoroutine = null;
    }
    public IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TimerSeconds++;
            OnGameTimerTick.Invoke(TimerSeconds);
        } 
    }

    public void ChangeTimeScale(float timeScale)
    {
        _gameTimeScale = timeScale;
        Time.timeScale = timeScale;
    }
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); 
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePause.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = _gameTimeScale;
        OnGameResume.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
