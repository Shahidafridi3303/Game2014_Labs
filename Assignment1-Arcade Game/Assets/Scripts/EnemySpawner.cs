using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private bool spawnOnXAxis; // Boolean to determine spawning axis
    [SerializeField] private float minRange, maxRange;
    [SerializeField] private float largeEnemySpawnChance = 0.2f; // Chance to spawn a large enemy

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition;

            // Determine the spawn position based on the boolean
            if (spawnOnXAxis)
            {
                // Spawn randomly along the x-axis within the horizontal bounds, y remains the same as the spawner's y position
                spawnPosition = new Vector2(Random.Range(minRange, maxRange), transform.position.y);
            }
            else
            {
                // Spawn randomly along the y-axis within the vertical bounds, x remains the same as the spawner's x position
                spawnPosition = new Vector2(transform.position.x, Random.Range(minRange, maxRange));
            }

            // Determine if a large enemy should be spawned
            bool isLargeEnemy = Random.Range(0f, 1f) < largeEnemySpawnChance;

            // Get an enemy from the pool
            GameObject enemy = EnemyPool.Instance.GetPooledEnemy(isLargeEnemy);
            if (enemy != null)
            {
                enemy.transform.position = spawnPosition;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
            }

            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}