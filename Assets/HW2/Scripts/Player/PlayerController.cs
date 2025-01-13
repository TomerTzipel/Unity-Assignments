using HW1;
using HW2;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    [SerializeField] private PlayerHealthHandler playerHealthHandler;
    [SerializeField] private PlayerFlashHandler playerFlashHandler;

    public UnityEvent<int> OnPlayerHit;
    public event UnityAction<int> OnPlayerHitAction;

    public event UnityAction<BarArgs> OnPlayerLoad;

    public event UnityAction<int> OnPlayerTookDamage { add { playerHealthHandler.OnPlayerTookDamage += value; } remove { playerHealthHandler.OnPlayerTookDamage -= value; } }
    public event UnityAction OnPlayerDeath { add { playerHealthHandler.OnPlayerDeath += value; } remove { playerHealthHandler.OnPlayerDeath -= value; } }
    void Awake()
    {
        playerMovementHandler.PlayerSettings = playerSettings;
        playerHealthHandler.PlayerSettings = playerSettings;
        playerFlashHandler.PlayerSettings = playerSettings;

        OnPlayerLoad.Invoke(new BarArgs{startValue = playerSettings.MaxHP, maxValue = playerSettings.MaxHP, minValue = 0 });

        OnPlayerHitAction += playerHealthHandler.TakeDamage;
        OnPlayerHit.AddListener(OnPlayerHitAction);

        OnPlayerDeath += PlayerDeath;

        playerFlashHandler.OnPlayerFlash.AddListener(playerMovementHandler.StopMoving);
    }

    public void CheckForPlayerHit(BulletCollisionArgs args)
    {
        if (args.objectHit.CompareTag("Player"))
        {
            OnPlayerHit.Invoke(args.damage);
        }
    }

    public void PlayerDeath()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void OnMoveCommand(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerMovementHandler.Move();
    }
    public void OnFlashCommand(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        playerFlashHandler.Flash();
    }
}
