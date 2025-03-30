
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ShooterHandler : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] Transform spawnPoint;
    [SerializeField] ShooterSettings shooterSettings;

    //Fields:
    private float _fireRate;
    private List<BulletHandler> _bulletPool;

    //Events:
    public event UnityAction<BulletHandler> OnBulletSpawn;

    private void Awake()
    {
        _fireRate = Random.Range(shooterSettings.MinFireRate, shooterSettings.MaxFireRate);
        _bulletPool = new List<BulletHandler>(shooterSettings.PoolSize);
    }

    private void Start()
    {
        //Needs to be in start due to BulletManager subscribing in his awake to OnBulletSpawn
        for (int i = 0; i < shooterSettings.PoolSize; i++)
        {
            _bulletPool.Add(CreateBullet());
        }

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
        BulletHandler availableBullet = _bulletPool.Find(handler => !handler.enabled);
        if (availableBullet == null)
        {
            availableBullet = CreateBullet();
            _bulletPool.Add(availableBullet);
        }

        availableBullet.enabled = true;
        availableBullet.gameObject.SetActive(true);
        availableBullet.transform.position = spawnPoint.position;
    }

    private BulletHandler CreateBullet()
    {
        BulletHandler bullet = Instantiate(shooterSettings.BulletPrefab, spawnPoint.position, Quaternion.identity, transform);
        bullet.Direction = transform.forward;
        OnBulletSpawn.Invoke(bullet);
        bullet.enabled = false;
        bullet.gameObject.SetActive(false);
        return bullet;
    }
}
