using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


namespace HW2
{
    public enum PowerUpType
    {
        Heal,
        SlowTime,
        Invulnerable
    }

    [CreateAssetMenu(fileName = "PowerUpGenerator", menuName = "Scriptable Objects/Power Up/PowerUpGenerator")]
    public class PowerUpGenerator : ScriptableObject
    {
        [Serializable]
        public struct PowerUpData
        {
            public PowerUpType PowerUpType;
            public PowerUp PowerUpPrefab;
            public int Weight;
        }

        [field: SerializeField] public List<PowerUpData> PowerUps { get; private set; }

        public PowerUp GetPowerUpByType(PowerUpType type)
        {
            int index = PowerUps.FindIndex((t) => t.PowerUpType == type);
            if (index == -1)
            {
                throw new System.Exception("Power up is not in the list");
            }
            return PowerUps[index].PowerUpPrefab;

        }

        public PowerUp GetRandomPowerUp()
        {
            int[] weightArray = new int[PowerUps.Count];
            int totalWeight = 0;
            for (int i = 0; i < PowerUps.Count; i++)
            {
                totalWeight += PowerUps[i].Weight;
                weightArray[i] = totalWeight;
            }
            int rnd = Random.Range(0, totalWeight);

            for (int i = 0; i < weightArray.Length; i++)
            {
                if (rnd < weightArray[i])
                {
                    return PowerUps[i].PowerUpPrefab;
                }

            }
            throw new Exception("Error in weight table");
        }
    }
}


