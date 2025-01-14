using UnityEngine;

[CreateAssetMenu(fileName = "SlowTimeData", menuName = "Scriptable Objects/SlowTimeData")]
public class SlowTimeData : ScriptableObject
{
    [field:SerializeField] [field:Range(0, 1)] public float TimeSlow { get; private set; }

    [field:SerializeField] public int Duration { get; private set; }

   


}
