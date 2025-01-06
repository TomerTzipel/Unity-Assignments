using UnityEngine;
using UnityEngine.Events;
namespace HW2
{
    public class Bullet : MonoBehaviour
    {
        public event UnityAction<BulletCollisionArgs> OnBulletHit;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private BulletSettings bulletSettings;
        private float _speed;
        public Vector3 Direction { get; set; }

        void Awake()
        {
            _speed = Random.Range(bulletSettings.MinSpeed, bulletSettings.MaxSpeed);
        }

        void FixedUpdate()
        {
            Vector3 move = _speed * Time.fixedDeltaTime * Direction;
            rb.MovePosition(transform.position + move);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnBulletHit.Invoke(new BulletCollisionArgs { damage = bulletSettings.BulletDamage, bullet = this, objectHit = collision.gameObject });
        }
    }

    public struct BulletCollisionArgs
    {
        public int damage;
        public Bullet bullet;
        public GameObject objectHit;
    }

}
