using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] private List<ShooterHandler> shooters;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int damageMultiplerBuffInterval;
    [SerializeField] private int damageMultiplerBuff;

    //Fields:
    private float _damageMultipler;

    private void Awake()
    {
        GameManager.Instance.OnLoadGame += HandleLoadGame;

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

        _damageMultipler += damageMultiplerBuff;
    }

    private void OnBulletSpawn(BulletHandler bullet)
    {
        bullet.OnBulletHit += HandleBulletHit;
    }

    private void HandleBulletHit(BulletCollisionArgs args)
    {
        if (args.objectHit.CompareTag("Player"))
        {
            int damage = (int)(args.damage * _damageMultipler);
            playerController.HitPlayer(damage);     
        }
        args.bullet.gameObject.SetActive(false);
        args.bullet.enabled = false;
    }

    private void HandleLoadGame(SaveData data)
    {
        int numberOfBuffs = data.gameTime / damageMultiplerBuffInterval;
        _damageMultipler += numberOfBuffs * damageMultiplerBuff;
    }
}
