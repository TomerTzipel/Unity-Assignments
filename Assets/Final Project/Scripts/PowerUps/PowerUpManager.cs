
using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    //Serialized Fields:
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PowerUpGenerator powerUpGenerator;
    [SerializeField] private Transform powerUpParent;
    [SerializeField] private Rect spawnArea;
    [SerializeField] private int spawnRate;
    [SerializeField] private int healSpawnRate;
    [SerializeField][field: Range(0.1f, 0.9f)] private float slowTimeMultiplier;

    //Fields:
    private int _nextHealPowerUpSpawnCounter = 0;

    void Start()
    {
        StartCoroutine(PickupSpawner());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3(spawnArea.x, 3, spawnArea.y);
        Vector3 size = new Vector3(spawnArea.width, 0.1f, spawnArea.height);

        Gizmos.DrawWireCube(center, size);
    }

    private IEnumerator PickupSpawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(spawnRate);
            _nextHealPowerUpSpawnCounter++;

            if (_nextHealPowerUpSpawnCounter == healSpawnRate)
            {
                SpawnPowerUp(powerUpGenerator.GetPowerUpByType(PowerUpType.Heal));
                continue;
            }

            SpawnPowerUp(powerUpGenerator.GetRandomPowerUp());
        }
    }

    private void SpawnPowerUp(PowerUp powerUpPrefab)
    {
        Vector3 position = GetSpawnPosition();
        PowerUp spawnedPowerUp = Instantiate(powerUpPrefab, position, Quaternion.identity, powerUpParent);
        spawnedPowerUp.OnPowerUpEffect += playerController.ActivateEffect;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-spawnArea.width / 2, spawnArea.width / 2) + spawnArea.x;
        pos.z = Random.Range(-spawnArea.height / 2, spawnArea.height / 2) + spawnArea.y;
        pos.y = 1.2f;

        return pos;
    }
}
