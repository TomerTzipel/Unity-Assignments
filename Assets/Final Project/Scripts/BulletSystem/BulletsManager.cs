using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] private List<ShooterHandler> shooters;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int damageMultiplerBuffInterval;

    //Fields:
    private float _damageMultipler;

    private void Awake()
    {
        _damageMultipler = 1f;

        foreach (ShooterHandler shooter in shooters)
        {
            shooter.OnBulletSpawn += OnBulletSpawn;
        }
    }
    private void Start()
    {
        GameManager.Instance.OnGameTimerTick += HandleTimerSecondTick;
    }


    private void HandleTimerSecondTick(int time)
    {
        if (time == 0) return;

        if (time % damageMultiplerBuffInterval != 0) return;

        _damageMultipler += 0.1f;
    }

    private void OnBulletSpawn(BulletHandler bullet)
    {
        bullet.OnBulletHit += HandleBulletHit;
    }

    private void HandleBulletHit(BulletCollisionArgs args)
    {
        Debug.Log("We HIT SOMETHING");
        if (args.objectHit.CompareTag("Player"))
        {
            Debug.Log("We HIT PLAYER");
            int damage = (int)(args.damage * _damageMultipler);
            playerController.HitPlayer(damage);
            Destroy(args.bullet.gameObject);
        }     
    }
}
