using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
    [field:SerializeField] public PlayerController Player { get; private set; }
    [SerializeField] private PowerUpList powerUpList;
    [SerializeField] private Transform powerUpParent;
    [SerializeField] private Rect spawnArea;
    [SerializeField] private int spawnRate;




    private void Awake()
    {
        if(!Instance.IsUnityNull())
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
         
        }

    }

    private void Start()
    {
        StartCoroutine(PickupSpawner());
    }


    private void SpawnPowerUp()
    {
        PowerUp powerUp = powerUpList.GetRandomPowerUp();
        Vector3 position = GetSpawnPosition();
        Instantiate(powerUp, position, Quaternion.identity, powerUpParent);
    }

    private void SpawnPowerUp(PowerUpsEnum powerUpEnum)
    {
        PowerUp powerUp = powerUpList.GetPowerUpByEnum(powerUpEnum);
        Vector3 position = GetSpawnPosition();
        Instantiate(powerUp, position, Quaternion.identity, powerUpParent);
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-spawnArea.width / 2, spawnArea.width / 2) + spawnArea.x;
        pos.z = Random.Range(-spawnArea.height / 2, spawnArea.height / 2) + spawnArea.y;
        pos.y = 1.2f;

        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3(spawnArea.x, 3, spawnArea.y);
        Vector3 size = new Vector3(spawnArea.width, 0.1f, spawnArea.height);

        Gizmos.DrawWireCube(center, size);
    }

    public void SlowTime(float timeScale, int duration)
    {
        StartCoroutine(SlowTimeCoroutine(timeScale, duration));
    }

    private IEnumerator SlowTimeCoroutine(float timeScale, int duration)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }

    private IEnumerator PickupSpawner()

    {
        while (true)
        {
            SpawnPowerUp();
            yield return new WaitForSecondsRealtime(spawnRate);
        }
    }
}
