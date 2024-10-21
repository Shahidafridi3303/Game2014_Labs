using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private bool spawnOnXAxis;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    [SerializeField] private float largeEnemySpawnChance = 0.2f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition = DetermineSpawnPosition();
            bool isLargeEnemy = ShouldSpawnLargeEnemy();

            SpawnEnemy(spawnPosition, isLargeEnemy);

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private Vector2 DetermineSpawnPosition()
    {
        if (spawnOnXAxis)
        {
            return new Vector2(Random.Range(minRange, maxRange), transform.position.y);
        }
        else
        {
            return new Vector2(transform.position.x, Random.Range(minRange, maxRange));
        }
    }

    private bool ShouldSpawnLargeEnemy()
    {
        return Random.Range(0f, 1f) < largeEnemySpawnChance;
    }

    private void SpawnEnemy(Vector2 position, bool isLargeEnemy)
    {
        GameObject enemy = EnemyPool.Instance.GetPooledEnemy(isLargeEnemy);
        if (enemy != null)
        {
            enemy.transform.position = position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
        }
    }
}