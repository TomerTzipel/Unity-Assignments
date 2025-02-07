using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace HW2
{
    public class PlayerFlashHandler : PlayerHandlerScript
    {
        [SerializeField] private NavMeshAgent agent;

        public UnityEvent OnPlayerFlash;

        private bool _isFlashAvailable = true;

        public void Flash()
        {
            if (!_isFlashAvailable) return;

            OnPlayerFlash.Invoke();
            StartCoroutine(StartFlashCD(PlayerSettings.FlashCD));

            Vector3 position = transform.position;
            float distanceToPlayer = Vector3.Distance(Camera.main.transform.position, new Vector3(position.x, position.y - 1f, position.y));
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = distanceToPlayer;
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 position2D = new Vector2(position.x, position.z);
            Vector2 direction = (new Vector2(mouseWorldPoint.x, mouseWorldPoint.z) - new Vector2(position2D.x, position2D.y)).normalized;

            Vector2 newPosition = position2D + (direction * PlayerSettings.FlashDistance);
            agent.Warp(new Vector3(newPosition.x, position.y, newPosition.y));
        }

        private IEnumerator StartFlashCD(float cooldown)
        {
            _isFlashAvailable = false;
            yield return new WaitForSeconds(cooldown);
            _isFlashAvailable = true;
        }
    }
}


