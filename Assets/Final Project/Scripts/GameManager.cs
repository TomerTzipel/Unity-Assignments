using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Save Data Classes
[Serializable]
public class SaveData
{
    public PlayerSaveData playerSaveData;
    public AudioSaveData audioSaveData;
    public int gameTime;
}
[Serializable]
public class PlayerSaveData
{
    public int playerHP;
    public float playerX;
    public float playerZ;
    public int playerScore;
}
[Serializable]
public class AudioSaveData
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //SerializedField
    [SerializeField] private string saveFileName;

    //Fields:
    public SaveData GameSaveData { get; set; } = new SaveData();
    public bool ShouldLoadGame { get; set; } = false;
    private FileDataHandler _dataHandler;



    public int GameEndScore { get; set; }
    public int TimerSeconds { get; private set; }
    private float _gameTimeScale;
    private Coroutine _gameTimerCoroutine;
   
    //Events:
    public event UnityAction<int> OnGameTimerTick;
    public event UnityAction OnGamePause;
    public event UnityAction OnGameResume;

    public event UnityAction OnSaveGame;
    public event UnityAction<SaveData> OnLoadGame;



    private void Awake()
    {
        if (!Instance.IsUnityNull())
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        _gameTimeScale = 1f;

        _dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);
    }

    public void StartGame()
    {
        if (ShouldLoadGame)
        {
            LoadGame();
            _gameTimerCoroutine = StartCoroutine(RunTimer());
        }
        else
        {
            StartGameTimer();
        }
       
    }
  
    public void StopGameTimer()
    {
        StopCoroutine(_gameTimerCoroutine);
        _gameTimerCoroutine = null;
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
    public void LoadGame()
    {
        ShouldLoadGame = false;
        SaveData data = _dataHandler.Load();
        if (data == null) return;

        TimerSeconds = data.gameTime;
        OnLoadGame.Invoke(data);

    }
    private void StartGameTimer()
    {
        TimerSeconds = 0;
        _gameTimerCoroutine = StartCoroutine(RunTimer());
    }
    private void SaveGame()
    {
        //After this event invocation all the data is entered to the GameSaveData property from the subscribers (PlayerController & AudioManager)
        OnSaveGame.Invoke();
        GameSaveData.gameTime = TimerSeconds;
        _dataHandler.Save(GameSaveData);
    }

    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TimerSeconds++;
            OnGameTimerTick.Invoke(TimerSeconds);

            //Save every minute
            if (TimerSeconds % 60 == 0) SaveGame();
        }
    }
}
