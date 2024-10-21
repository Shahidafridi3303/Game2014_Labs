using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject largeEnemyPrefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private bool canExpand = true;

    private List<GameObject> enemyPool;
    private List<GameObject> largeEnemyPool;

    private void Awake()
    {
        SetupSingletonInstance();
    }

    private void Start()
    {
        InitializePools();
    }

    private void SetupSingletonInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePools()
    {
        enemyPool = new List<GameObject>(initialPoolSize);
        largeEnemyPool = new List<GameObject>(initialPoolSize / 2); // Adjust size as needed

        for (int i = 0; i < initialPoolSize; i++)
        {
            AddEnemyToPool();
        }

        for (int i = 0; i < initialPoolSize / 2; i++)
        {
            AddLargeEnemyToPool();
        }
    }

    private GameObject AddEnemyToPool()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false);
        enemyPool.Add(enemy);
        return enemy;
    }

    private GameObject AddLargeEnemyToPool()
    {
        GameObject largeEnemy = Instantiate(largeEnemyPrefab);
        largeEnemy.SetActive(false);
        largeEnemyPool.Add(largeEnemy);
        return largeEnemy;
    }

    public GameObject GetPooledEnemy(bool isLarge)
    {
        List<GameObject> pool = isLarge ? largeEnemyPool : enemyPool;

        GameObject enemy = FindInactiveEnemy(pool);
        if (enemy != null)
        {
            return enemy;
        }

        if (canExpand)
        {
            return isLarge ? AddLargeEnemyToPool() : AddEnemyToPool();
        }

        return null;
    }

    private GameObject FindInactiveEnemy(List<GameObject> pool)
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}