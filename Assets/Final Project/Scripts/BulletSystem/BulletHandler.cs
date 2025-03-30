
using UnityEngine;
using UnityEngine.Events;

public struct BulletCollisionArgs
{
    public int damage;
    public BulletHandler bullet;
    public GameObject objectHit;
}

public class BulletHandler : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] private BulletSettings bulletSettings;

    //Fields:
    public Vector3 Direction { get; set; }
    private float _speed;
 
    //Events:
    public event UnityAction<BulletCollisionArgs> OnBulletHit;

    void Awake()
    {
        _speed = Random.Range(bulletSettings.MinSpeed, bulletSettings.MaxSpeed);
    }

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        OnBulletHit.Invoke(new BulletCollisionArgs { damage = bulletSettings.BaseBulletDamage, bullet = this, objectHit = other.gameObject });
    }
}
