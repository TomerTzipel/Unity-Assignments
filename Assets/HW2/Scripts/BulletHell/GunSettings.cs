using UnityEngine;

namespace HW2
{
    [CreateAssetMenu(fileName = "GunSettings", menuName = "Dodgeball/GunSettings")]
    public class GunSettings : ScriptableObject
    {
        [field: SerializeField] public BulletHandler BulletPrefab { get; private set; }
        [field: SerializeField] public float MinFireRate { get; private set; }
        [field: SerializeField] public float MaxFireRate { get; private set; }
    }
}

