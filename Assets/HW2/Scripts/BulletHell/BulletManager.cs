using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HW2
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private List<Gun> guns;
        [SerializeField] private PlayerManager playerManager;

        private void Awake()
        {
            foreach (Gun gun in guns)
            {
                gun.OnBulletSpawn += OnBulletSpawn;
            }

           
        }

        private void OnBulletSpawn(Bullet bullet)
        {
            bullet.OnBulletHit += DestroyBullet;
            bullet.OnBulletHit += playerManager.CheckForPlayerHit;
        }

        private void DestroyBullet(BulletCollisionArgs args)
        {
            Destroy(args.bullet.gameObject);
        }
    }
}

