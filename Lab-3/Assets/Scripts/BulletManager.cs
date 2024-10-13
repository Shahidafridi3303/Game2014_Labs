using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] 
    int _totalPlayerBulletNum;
    [SerializeField]
    int _totalEnemyBulletNum;
    Queue<GameObject> _playerBulletPool = new Queue<GameObject>();
    Queue<GameObject> _enemyBulletPool = new Queue<GameObject>();

    private Dictionary<BulletType, Queue<GameObject>> _bulletPoolDictionary = new Dictionary<BulletType, Queue<GameObject>>();
    [SerializeField]
    Dictionary<BulletType, int> _bulletPoolSizes = new Dictionary<BulletType, int>();

    BulletFactory _bulletFactory;

    // Start is called before the first frame update
    void Awake()
    {
        _bulletFactory = FindObjectOfType<BulletFactory>();

        _bulletPoolSizes[BulletType.PLAYER] = _totalPlayerBulletNum;
        _bulletPoolSizes[BulletType.ENEMY] = _totalEnemyBulletNum;
        //Build bullet pool
        PoolBuilder();
    }

    void PoolBuilder()
    {
        for (int i = 0; i < (int)BulletType.SIZE; i++)
        {
            BulletType type = (BulletType)i;
            _bulletPoolDictionary[type] = new Queue<GameObject>();

            for (int j = 0; j < _bulletPoolSizes[type]; j++)
            {
                _bulletPoolDictionary[type].Enqueue(_bulletFactory.CreateBullet(type));
            }
        }
    }

    public GameObject GetBullet(BulletType type)
    {
        if (_bulletPoolDictionary.Count <= 1)
        {
            //in the stress point so create more bullet
            _bulletPoolDictionary[type].Enqueue(_bulletFactory.CreateBullet(type));
        }

        GameObject bullet = _bulletPoolDictionary[type].Dequeue();
        bullet.SetActive(true);

        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        _bulletPoolDictionary[type].Enqueue(bullet);
    }
}




