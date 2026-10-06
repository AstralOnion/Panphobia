using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval;
    public float spawnDistanceMin;
    public float spawnDistanceMax;

    [Header("Enemy Lifetime")]
    public float enemyLifetime = 10f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        Debug.Log("Starting coroutine");

        while (true)
        {
            Vector3 currentPosition = transform.position;

            float x = Random.Range(spawnDistanceMin, spawnDistanceMax);

            int xNegative = Random.Range(0, 2);

            if (xNegative == 1)
            {
                x = -x;
            }

            float y = Random.Range(spawnDistanceMin, spawnDistanceMax);

            int yNegative = Random.Range(0, 2);

            if (yNegative == 1)
            {
                y = -y;
            }

            Vector3 spawnPosition = new Vector3(x, y, 0) + currentPosition;

            GameObject spawnedEnemy = Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );

            Destroy(spawnedEnemy, enemyLifetime);

            Debug.Log("Spawned enemy at: " + spawnPosition);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
