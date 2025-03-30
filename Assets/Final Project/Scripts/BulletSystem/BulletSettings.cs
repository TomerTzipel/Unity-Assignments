using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettings", menuName = "Shooter/BulletSettings")]
public class BulletSettings : ScriptableObject
{
    [field: SerializeField] public float MinSpeed { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public int BaseBulletDamage { get; private set; }
}
