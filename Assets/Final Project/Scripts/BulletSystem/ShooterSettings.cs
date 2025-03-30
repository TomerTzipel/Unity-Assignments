using UnityEngine;

[CreateAssetMenu(fileName = "ShooterSettings", menuName = "Shooter/ShooterSettings")]
public class ShooterSettings : ScriptableObject
{
    [field: SerializeField] public BulletHandler BulletPrefab { get; private set; }
    [field: SerializeField] public float MinFireRate { get; private set; }
    [field: SerializeField] public float MaxFireRate { get; private set; }

    [field: SerializeField] public int PoolSize { get; private set; }
}
