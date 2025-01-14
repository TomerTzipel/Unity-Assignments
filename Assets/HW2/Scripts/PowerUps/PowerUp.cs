using UnityEngine;
using System.ComponentModel;
public abstract class PowerUp : MonoBehaviour
{

    public PowerUpsEnum Enum;

    protected void Awake()
    {
        gameObject.name = this.GetType().Name;
    }

    public abstract void OnPickUp();

    private void OnTriggerEnter(Collider other)
    {
        OnPickUp();
        Destroy(gameObject);
    }

}
