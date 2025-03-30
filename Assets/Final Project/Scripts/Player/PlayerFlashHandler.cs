using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerFlashHandler : PlayerHandlerScript
{


    //Serialized Fields:
    [SerializeField] ParticleSystem flashParticleSystemEffect;


    //Fields:
    public UnityAction OnPlayerFlash;
    private bool _isFlashAvailable = true;

    public void Flash()
    {
        if (!_isFlashAvailable) return;

        OnPlayerFlash.Invoke();
        StartCoroutine(StartFlashCD(PlayerSettings.FlashCD));
        PlayFlashEffect();
        AudioManager.Instance.PlaySfx(SFX.Flash);

        Vector3 position = transform.position;
        float distanceToPlayer = Vector3.Distance(Camera.main.transform.position, new Vector3(position.x, position.y - 1f, position.y));
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = distanceToPlayer;
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 position2D = new Vector2(position.x, position.z);
        Vector2 direction = (new Vector2(mouseWorldPoint.x, mouseWorldPoint.z) - new Vector2(position2D.x, position2D.y)).normalized;

        Vector2 newPosition = position2D + (direction * PlayerSettings.FlashDistance);
        NavMeshAgent.Warp(new Vector3(newPosition.x, position.y, newPosition.y));
    }
     
    private void PlayFlashEffect()
    {
        Vector3 spawnPosition = transform.position;
        ParticleSystem flashEffect = Instantiate(flashParticleSystemEffect, spawnPosition, Quaternion.identity);

        if (flashEffect != null)
        {
            flashEffect.Play();
            Destroy(flashEffect.gameObject, 2f);
        }      
    }

    private IEnumerator StartFlashCD(float cooldown)
    {
        _isFlashAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _isFlashAvailable = true;
    }
}
