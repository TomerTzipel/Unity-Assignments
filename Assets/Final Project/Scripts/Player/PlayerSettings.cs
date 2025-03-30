using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Player/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [field: SerializeField, Header("Stats")] public int MaxHP { get; private set; }
    [field: SerializeField] public float InvulDuration { get; private set; }
    [SerializeField][field: Range(0.1f, 0.9f)] public float SlowTimeMultiplier { get; private set; }

    [field: SerializeField, Header("Movement")] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float AccelerationSpeed { get; private set; }

    [SerializeField] private float _destinationOffset;
    public float DestinationOffsetSqrd
    {
        get { return _destinationOffset * _destinationOffset; }
    }
   

    [field: SerializeField, Header("Flash")] public float FlashCD { get; private set; }
    [field: SerializeField] public float FlashDistance { get; private set; }

    [field: SerializeField, Header("Score Points")] public int PowerUpScore { get; private set; }
    [field: SerializeField] public int TimerScore { get; private set; }
    [field: SerializeField] public int HitScore { get; private set; }
}
