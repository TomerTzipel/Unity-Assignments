using UnityEngine;

public class HealthPickUp : PowerUp
{
    [SerializeField] private float healingAmount = 10;

    

    public override void OnPickUp()
    {
        //GameManager.Instance.Player.getHeal (healing amount)
        Destroy(gameObject);
    }


    
}
