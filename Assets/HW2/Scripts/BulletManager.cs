using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
namespace HW2
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private List<Gun> guns;

        private void Awake()
        {
            foreach (Gun gun in guns)
            {
                gun.OnBulletSpawn += SubscribeToBullet;
            }
        }

        public void SubscribeToBullet(Bullet bullet)
        {
            bullet.OnBulletHit += DestroyBullet;
        }

        private void DestroyBullet(BulletCollisionArgs args)
        {
            //Check if the target hit is the player
            //Tell player he took damage
            Debug.Log("Destroy bullet");
            Destroy(args.bullet.gameObject);
        }
    }
}

