using UnityEngine;

public class SlowTimePickUp : PowerUp
{

    [SerializeField] private SlowTimeData data;

    public override void OnPickUp()
    {
        GameManager.Instance.SlowTime(data.TimeSlow, data.Duration);
    }
}
