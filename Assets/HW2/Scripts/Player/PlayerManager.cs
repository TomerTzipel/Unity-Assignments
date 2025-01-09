using HW1;
using HW2;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private PlayerMovementHandler playerMovementHandler;
    [SerializeField] private PlayerHealthHandler playerHealthHandler;
    [SerializeField] private UIManager uiManager;


    public UnityEvent<int> OnPlayerHit;
    public event UnityAction<int> OnPlayerHitAction;

    

    void Awake()
    {
        playerMovementHandler.PlayerSettings = playerSettings;
        playerHealthHandler.PlayerSettings = playerSettings;

        OnPlayerHitAction += playerHealthHandler.TakeDamage;
        OnPlayerHit.AddListener(OnPlayerHitAction);

        playerHealthHandler.OnPlayerTookDamage += uiManager.OnPlayerTakeDamage;
        playerHealthHandler.OnPlayerDeath += uiManager.OnPlayerDeath;
        playerHealthHandler.OnPlayerDeath += OnPlayerDeath;
        uiManager.BarValuesSetUp(new BarArgs { minValue = 0, maxValue = playerSettings.MaxHP, startValue = playerSettings.MaxHP });  
    }

    public void CheckForPlayerHit(BulletCollisionArgs args)
    {
        if (args.objectHit.CompareTag("Player"))
        {
            OnPlayerHit.Invoke(args.damage);
        }
    }

    public void OnPlayerDeath()
    {
        Debug.Log("In PM Death");
        transform.parent.gameObject.SetActive(false);
    }

    
}
