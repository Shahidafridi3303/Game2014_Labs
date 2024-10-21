using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 20;
    [SerializeField] private bool canExpand = true;

    private List<GameObject> bulletPool;

    private void Awake()
    {
        SetupSingletonInstance();
    }

    private void Start()
    {
        InitializeBulletPool();
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

    private void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>(initialPoolSize);
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddBulletToPool();
        }
    }

    private GameObject AddBulletToPool()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bulletPool.Add(bullet);
        return bullet;
    }

    public GameObject GetPooledBullet()
    {
        GameObject bullet = FindInactiveBullet();
        if (bullet != null)
        {
            return bullet;
        }

        if (canExpand)
        {
            return AddBulletToPool();
        }

        return null;
    }

    private GameObject FindInactiveBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}