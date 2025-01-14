using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public enum PowerUpsEnum
{
    Health,
    SlowTime,
    Invincibility,
}




[CreateAssetMenu(fileName = "Power Up List", menuName = "Scriptable Objects/Power Up/Power Up List")]
public class PowerUpList : ScriptableObject
{

    [Serializable]
    public class PowerUpData
    {
        public PowerUp PowerUp;
        public int Weight;

    }

    [field: SerializeField] public List<PowerUpData> PowerUps { get; private set; }


    public PowerUp GetPowerUpByEnum(PowerUpsEnum powerUpEnum)
    {
        int index = PowerUps.FindIndex((t) => t.PowerUp.Enum == powerUpEnum);
        if(index == -1)
        {
            throw new System.Exception("Power up is not in the list");
        }
        return PowerUps[index].PowerUp; 

    }


    public PowerUp GetRandomPowerUp()
    {
        int[] weightArray = new int[PowerUps.Count];
        int totalWeight = 0;
        for(int i = 0; i < PowerUps.Count; i++)
        {
            totalWeight += PowerUps[i].Weight;
            weightArray[i] = totalWeight;
        }
        int rnd = Random.Range(0, totalWeight);

        for(int i = 0; i < weightArray.Length; i++)
        {
            if(rnd < weightArray[i])
            {
                return PowerUps[i].PowerUp;
            }

        }

        throw new Exception("Error in weight table");
    }
}
