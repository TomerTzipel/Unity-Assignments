using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]

public class PlayerSettings : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private int maxHP;
    [SerializeField] private float movementSpeed; 
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float destinationOffset;

    [Header("Visuals")]
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material hurtMaterial;
    [SerializeField] private float hurtDuration;

    public int MaxHP { get { return maxHP; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float AccelerationSpeed { get { return accelerationSpeed; } }
    public float DestinationOffset { get { return destinationOffset; } }
    public Material PlayerMaterial { get { return playerMaterial; } }
    public Material HurtMaterial { get { return hurtMaterial; } }
    public float HurtDuration { get { return hurtDuration; } }
}
