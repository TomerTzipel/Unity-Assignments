using HW1;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using HW3;

namespace HW2
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;

        [Header("Handlers")]
        [SerializeField] private PlayerMovementHandler playerMovementHandler;
        [SerializeField] private PlayerHealthHandler playerHealthHandler;
        [SerializeField] private PlayerFlashHandler playerFlashHandler;

        public event UnityAction<PlayerSettings> OnPlayerLoad;

        public UnityEvent<int> OnPlayerHit;
        public event UnityAction<int> OnPlayerHitAction;

        //Tried to create the UnityActions in runtime for the dictionary but it didn't work and I couldn't find anyting about it online :(
        //Atleast ActivteEffect still looks tidy
        //Also not sure if event is necessery here or not, they are private but are accesible from the dictionary, so I guess event is needed.
        private event UnityAction<float> OnPlayerHeal;
        private event UnityAction<float> OnPlayerInvul;
        private event UnityAction<float> OnSlowTime;

        public Dictionary<PowerUpType, UnityAction<float>> EffectActions { get; } = new Dictionary<PowerUpType, UnityAction<float>>();
        public event UnityAction<PowerUpType> OnPlayerPowerUp;

        //Some actions are only invoked inside handlers but I need to allow other managers to subscribe to them
        public event UnityAction<int> OnPlayerTookDamage { add { playerHealthHandler.OnPlayerTookDamage += value; } remove { playerHealthHandler.OnPlayerTookDamage -= value; } }
        public event UnityAction<float> OnPlayerHealthChange { add { playerHealthHandler.OnPlayerHealthChange += value; } remove { playerHealthHandler.OnPlayerHealthChange -= value; } }
        public event UnityAction OnPlayerDeath { add { playerHealthHandler.OnPlayerDeath += value; } remove { playerHealthHandler.OnPlayerDeath -= value; } }

        public PlayerSettings PlayerSettings { get { return playerSettings; } }

        private void Awake()
        {
            
            EffectActions.Add(PowerUpType.Heal, OnPlayerHeal);
            EffectActions.Add(PowerUpType.Invincibility, OnPlayerInvul);
            EffectActions.Add(PowerUpType.SlowTime, OnSlowTime);

            OnPlayerDeath += PlayerDeath;

            //Hnadlers don't know each other so contoller subscribes them to each other's events
            playerFlashHandler.OnPlayerFlash.AddListener(playerMovementHandler.StopMoving);
        }

        private void Start()
        {
            //Adding OnPlayerHitAction listeners to OnPlayerHit UnityEvent
            //This is in start so functions subscribing to OnPlayerHitAction will be added as listeners in their awake
            //otherwise there will be timing issues, as adding a UnityAction to a UnityEvent doesn't work retroactively
            OnPlayerHit.AddListener(OnPlayerHitAction);

            //Only used for the HP bar but still makes sense to be an event
            OnPlayerLoad.Invoke(playerSettings);
        }

        public void CheckForPlayerHit(BulletCollisionArgs args)
        {
            if (args.objectHit.CompareTag("Player"))
            {
                OnPlayerHit.Invoke(args.damage);
            }
        }

   
        private void PlayerDeath()
        {
            Debug.Log("PlayerDeath() triggered!");

           
            playerHealthHandler.GetComponent<PlayerAnimationHandler>().ActivateDeathAnimation();
        }



        public void ActivateEffect(Effect effect)
        {
            if (!EffectActions.ContainsKey(effect.type)) return;

            EffectActions[effect.type].Invoke(effect.value);
            OnPlayerPowerUp.Invoke(effect.type);
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
}

