using System.Collections.Generic;
using UnityEngine;

namespace HW2
{
    public class BulletsManager : MonoBehaviour
    {
        [SerializeField] private List<GunHandler> guns;
        [SerializeField] private PlayerController playerController;

        private void Awake()
        {
            foreach (GunHandler gun in guns)
            {
                gun.OnBulletSpawn += OnBulletSpawn;
            }    
        }

        private void OnBulletSpawn(BulletHandler bullet)
        {
            bullet.OnBulletHit += playerController.CheckForPlayerHit;
            bullet.OnBulletHit += DestroyBullet;
        }

        private void DestroyBullet(BulletCollisionArgs args)
        {

            Destroy(args.bullet.gameObject);
        }
    }
}

