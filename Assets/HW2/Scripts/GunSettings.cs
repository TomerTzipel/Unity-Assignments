using UnityEngine;
namespace HW2
{
    [CreateAssetMenu(fileName = "GunSettings", menuName = "Dodgeball/GunSettings")]
    public class GunSettings : ScriptableObject
    {
        [SerializeField] Bullet bulletPrefab;
        [SerializeField] float minFireRate;
        [SerializeField] float maxFireRate;

        public Bullet BulletPrefab { get { return bulletPrefab; } }
        public float MinFireRate { get { return minFireRate; } }
        public float MaxFireRate { get { return maxFireRate; } }
    }
}

