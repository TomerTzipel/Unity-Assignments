using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Fields:
    public int TimerSeconds { get; private set; }
    private float _gameTimeScale;
    private Coroutine _gameTimerCoroutine;
   
    //Events:
    public event UnityAction<int> OnGameTimerTick;

    private void Awake()
    {
        if (!Instance.IsUnityNull())
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = _gameTimeScale;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
