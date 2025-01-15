using UnityEngine;


    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField, Header("Stats")] public int MaxHP { get; private set; }
        [field: SerializeField] public float InvulDuration { get; private set; }

        [field: SerializeField, Header("Movement")] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float AccelerationSpeed { get; private set; }
        [field: SerializeField] public float DestinationOffset { get; private set; }

        [field: SerializeField, Header("Flash")] public float FlashCD { get; private set; }
        [field:SerializeField] public float FlashDistance { get; private set; }

        [field: SerializeField, Header("Visuals")] public Material PlayerMaterial { get; private set; }
        [field:SerializeField] public Material HurtMaterial { get; private set; }
}




