using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGamePressed()
    {
        GameManager.Instance.ChangeScene(1);
    }
    public void QuitGamePressed()
    {
        GameManager.Instance.QuitGame();
    }
    
}
