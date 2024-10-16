using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private bool canExpand = true;

    private List<GameObject> enemyPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        enemyPool = new List<GameObject>(initialPoolSize);
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddEnemyToPool();
        }
    }

    private GameObject AddEnemyToPool()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false);
        enemyPool.Add(enemy);
        return enemy;
    }

    public GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        if (canExpand)
        {
            return AddEnemyToPool();
        }

        return null;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}