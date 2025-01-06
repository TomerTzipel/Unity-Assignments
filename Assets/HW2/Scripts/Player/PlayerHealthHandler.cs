using System.Collections;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private int _currentHP;
    public PlayerSettings PlayerSettings { get; set; }

    public void TakeDamage(int damage)
    {
        StartCoroutine(HurtVisual());

        _currentHP -= damage;
        if(_currentHP <= 0)
        {
            //Initiate death
        }
    }

    private IEnumerator HurtVisual()
    {
        meshRenderer.material = PlayerSettings.HurtMaterial;
        yield return new WaitForSeconds(PlayerSettings.HurtDuration);
        meshRenderer.material = PlayerSettings.PlayerMaterial;
    }
}
