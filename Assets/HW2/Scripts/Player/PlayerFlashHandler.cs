using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerFlashHandler : MonoBehaviour
{
    public event UnityAction OnFlash;

    private bool _isFlashAvailable = true;
    public PlayerSettings PlayerSettings { get; set; }



    public void OnFlashCommand(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnFlash.Invoke();
        StartFlashCD(PlayerSettings.FlashCD);


    }

    private IEnumerator StartFlashCD(float cooldown)
    {
        _isFlashAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _isFlashAvailable = true;
    }
}
