using UnityEngine;
using UnityEngine.Events;
namespace HW2
{
    public class BulletHandler : MonoBehaviour
    {
        public event UnityAction<BulletCollisionArgs> OnBulletHit;

        [SerializeField] private BulletSettings bulletSettings;
        private float _speed;
        public Vector3 Direction { get; set; }

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
            OnBulletHit.Invoke(new BulletCollisionArgs { damage = bulletSettings.BulletDamage, bullet = this, objectHit = other.gameObject });
        }
    }

    public struct BulletCollisionArgs
    {
        public int damage;
        public BulletHandler bullet;
        public GameObject objectHit;
    }

}
