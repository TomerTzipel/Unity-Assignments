using HW1;
using HW2;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    [SerializeField] private PlayerHealthHandler playerHealthHandler;

    public UnityEvent<int> OnPlayerHit;
    public event UnityAction<int> OnPlayerHitAction;

    void Awake()
    {
        playerMovementHandler.PlayerSettings = playerSettings;
        playerHealthHandler.PlayerSettings = playerSettings;

        OnPlayerHitAction += playerHealthHandler.TakeDamage; 
        OnPlayerHit.AddListener(OnPlayerHitAction);
    }

    public void CheckForPlayerHit(BulletCollisionArgs args)
    {
        if (args.objectHit.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            OnPlayerHit.Invoke(args.damage);
        }
    }
}
