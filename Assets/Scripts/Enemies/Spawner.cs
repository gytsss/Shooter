using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject sEnemy;
    [SerializeField] private GameObject soldier;
    [SerializeField] private int cantEnemy = 5;
    [SerializeField] private int cantStaticEnemy = 5;
    [SerializeField] private int cantSoldier = 5;

    private void Start()
    {
        ObjectPooling.PreLoad(enemy, cantEnemy);
        ObjectPooling.PreLoad(sEnemy, cantStaticEnemy);
        ObjectPooling.PreLoad(soldier, cantSoldier);

        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < cantEnemy; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(spawnPositions[i % spawnPositions.Length]);
            GameObject enemyObj = ObjectPooling.GetObject(enemy);
            enemyObj.transform.position = spawnPosition;
        }

        for (int i = 0; i < cantStaticEnemy; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(spawnPositions[(i + cantEnemy) % spawnPositions.Length]);
            GameObject enemyObj = ObjectPooling.GetObject(sEnemy);
            enemyObj.transform.position = spawnPosition;
        }

        for (int i = 0; i < cantSoldier; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(spawnPositions[(i + cantSoldier) % spawnPositions.Length]);
            GameObject enemyObj = ObjectPooling.GetObject(soldier);
            enemyObj.transform.position = spawnPosition;
        }

    }
     
    private Vector3 GetSpawnPosition(Transform spawnTransform)
    {
        return spawnTransform.position;
    }
}
