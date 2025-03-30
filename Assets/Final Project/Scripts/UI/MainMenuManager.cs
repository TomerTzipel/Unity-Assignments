using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;   

    private void Awake()
    {
        if (PlayerPrefs.GetInt("DoesSaveExist") == 0)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public void StartGamePressed()
    {
        GameManager.Instance.ChangeScene(1);
    }

    public void ContinueGamePressed()
    {
        if(PlayerPrefs.GetInt("DoesSaveExist") == 1)
        {
            GameManager.Instance.ShouldLoadGame = true;
            GameManager.Instance.ChangeScene(1);
        }
    }

    public void QuitGamePressed()
    {
        GameManager.Instance.QuitGame();
    }
    
}
