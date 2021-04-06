using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] shapes;
    private static System.Random random = new System.Random(System.Environment.TickCount);
    private static int poolSize = 0;

    /// <summary>
    /// Словарь с индексом фигуры и процентом её выпадения
    /// </summary>
    private Dictionary<int, int> shapeChances = null;

    public Dictionary<int, int> ShapeChances
    {
        get => shapeChances;
        set
        {
            shapeChances = value;
            poolSize = 0;

            foreach (KeyValuePair<int, int> p in shapeChances)
            {
                poolSize += p.Value;
            }
        }
    }

    /// <summary>
    /// Возвращает индекс фигуры в словаре с учётом процента её выпадения
    /// </summary>
    /// <returns></returns>
    private int GetShapeIndexByChance()
    {
        int randomNumber = random.Next(0, poolSize) + 1;
        int accumulatedProbability = 0;

        foreach (KeyValuePair<int, int> p in shapeChances)
        {
            accumulatedProbability += p.Value;

            if (randomNumber <= accumulatedProbability)
            {
                return p.Key;
            }
        }

        return 0;
    }

    public void SpawnNext()
    {
        Instantiate(shapes[GetShapeIndexByChance()], transform.position, Quaternion.identity);
    }
}
