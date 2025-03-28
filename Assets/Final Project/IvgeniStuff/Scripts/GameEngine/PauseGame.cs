using System.Collections;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void OnPauseGame()
    {
        StartCoroutine(onPasuedPressed());
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1f;
    }


    private IEnumerator onPasuedPressed()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
    }
}
