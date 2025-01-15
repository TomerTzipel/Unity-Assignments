using UnityEngine;

namespace HW2
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Dodgeball/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private int bulletDamage;

        public float MinSpeed { get { return minSpeed; } }
        public float MaxSpeed { get { return maxSpeed; } }
        public int BulletDamage { get { return bulletDamage; } }
    }
}

