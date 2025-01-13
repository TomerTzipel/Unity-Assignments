using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HW2
{
    public class GunHandler : MonoBehaviour
    {
        public event UnityAction<BulletHandler> OnBulletSpawn;
        [SerializeField] Transform spawnPoint;
        [SerializeField] GunSettings gunSettings;

        private float _fireRate;

        void Awake()
        {
            _fireRate = Random.Range(gunSettings.MinFireRate, gunSettings.MaxFireRate);
            StartCoroutine(FireCooldown());
        }

        private IEnumerator FireCooldown()
        {
            yield return new WaitForSeconds(_fireRate);
            Fire();
            StartCoroutine(FireCooldown());
        }

        private void Fire()
        {
            BulletHandler bullet = Instantiate(gunSettings.BulletPrefab, spawnPoint.position, Quaternion.identity, transform);
            bullet.Direction = transform.forward;
            OnBulletSpawn.Invoke(bullet);
        }

    }
}

