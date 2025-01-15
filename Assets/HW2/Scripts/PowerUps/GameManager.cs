using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace HW2
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerController playerController;
        [SerializeField] private PowerUpGenerator powerUpGenerator;
        [SerializeField] private Transform powerUpParent;
        [SerializeField] private Rect spawnArea;
        [SerializeField] private int spawnRate;
        [SerializeField] private int healSpawnRate;
        [SerializeField][field: Range(0.1f, 0.9f)] private float slowTimeMultiplier;
        private int _nextHealPowerUpSpawnCounter = 0;

        private void Awake()
        {
            if (!Instance.IsUnityNull())
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            Time.timeScale = 1f;
            playerController.EffectActions[PowerUpType.SlowTime] += OnSlowTime;
        }

        private void Start()
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

        private void OnSlowTime(float duration)
        {
            StartCoroutine(SlowTimeCoroutine(duration));
        }

        private IEnumerator SlowTimeCoroutine(float duration)
        {
            Time.timeScale = slowTimeMultiplier;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1;
        }
    }

}

