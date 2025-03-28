using UnityEngine;

namespace HW2
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Dodgeball/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public int BulletDamage { get; private set; }

    }
}

