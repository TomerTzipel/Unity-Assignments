using UnityEngine;

public class InvincibilityPickUp : PowerUp
{

    [SerializeField] private int duration;

    public override void OnPickUp()
    {
        throw new System.NotImplementedException();
    }
}
