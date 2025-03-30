
using HW2;
using System.Collections;
using System.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class ShooterHandler : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] Transform spawnPoint;
    [SerializeField] ShooterSettings shooterSettings;

    //Fields:
    private float _fireRate;

    //Events:
    public event UnityAction<BulletHandler> OnBulletSpawn;

    void Awake()
    {
        _fireRate = Random.Range(shooterSettings.MinFireRate, shooterSettings.MaxFireRate);
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
        BulletHandler bullet = Instantiate(shooterSettings.BulletPrefab, spawnPoint.position, Quaternion.identity, transform);
        bullet.Direction = transform.forward;
        OnBulletSpawn.Invoke(bullet);
    }
}
